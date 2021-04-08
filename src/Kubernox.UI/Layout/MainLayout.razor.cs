using AntDesign;
using Fluxor;
using Kubernox.UI.Store.Actions.Cluster;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.Actions.DomainName;
using Kubernox.UI.Store.Actions.SshKey;
using Kubernox.UI.Store.Actions.Template;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
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

        public int NotificationsCount { get; set; } = 10;

        protected override async Task OnInitializedAsync()
        {

            Dispatcher.Dispatch(new FetchTemplateAction());
            Dispatcher.Dispatch(new FetchSshKeyAction());
            Dispatcher.Dispatch(new FetchClustersAction());
            Dispatcher.Dispatch(new FetchDatacenterAction());
            Dispatcher.Dispatch(new FetchDomainNameAction());

            connection = new HubConnectionBuilder()
                                .WithUrl($"{BaseUri}ws/notifications")
                                .Build();
            BindWsCallbacks();

            await connection.StartAsync();
        }

        private void BindWsCallbacks()
        {
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

                Dispatcher.Dispatch(new FetchClustersAction());
            });

            connection.On("StateChange", (string type) =>
            {
                switch (type)
                {
                    case "Cluster":
                        Dispatcher.Dispatch(new FetchClustersAction());
                        break;

                    case "Ssh":
                        Dispatcher.Dispatch(new FetchSshKeyAction());
                        break;

                    case "Template":
                        Dispatcher.Dispatch(new FetchTemplateAction());
                        break;
                }
            });
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
