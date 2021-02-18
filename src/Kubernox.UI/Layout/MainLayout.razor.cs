using AntDesign;
using Fluxor;
using Kubernox.UI.Store.Actions.Cluster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {

        public static string BaseUri;

        HubConnection connection;
        
        [Inject]
        IDispatcher Dispatcher { get; set; }

        [Inject]
        NotificationService NotificationService { get; set; }

        public int NotificationsCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            connection = new HubConnectionBuilder()
                                .WithUrl($"{BaseUri}ws/notifications")
                                .Build();

            connection.On("NotificationReceived", async (string title, string content, string type) =>
            {
                await NotificationService.Open(new NotificationConfig()
                {
                    Message = title,
                    Description = content,
                    NotificationType = GetNotificationType(type),
                    Duration = 8
                });

                NotificationsCount += 1;

                StateHasChanged();

                Dispatcher.Dispatch(new FetchClusterAction());
            });

            await connection.StartAsync();
        }

        private NotificationType GetNotificationType(string type)
        {
            switch (type)
            {
                case "success":
                    return NotificationType.Success;
                case "Info":
                    return NotificationType.Info;
                case "error":
                    return NotificationType.Error;
                case "Warning":
                    return NotificationType.Warning;
                default:
                    return NotificationType.None;
            }
        }
    }
}
