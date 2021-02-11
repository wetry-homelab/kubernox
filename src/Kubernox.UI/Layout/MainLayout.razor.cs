using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.UI.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {

        HubConnection connection;

        [Inject]
        NotificationService NotificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            connection = new HubConnectionBuilder()
                                .WithUrl("https://localhost:5001/app")
                                .Build();

            connection.On("NotificationReceived", async (string title, string content, string type) =>
            {
                Console.WriteLine("Notification Received");
                await NotificationService.Open(new NotificationConfig()
                {
                    Message = title,
                    Description = content,
                    NotificationType = GetNotificationType(type),
                    Duration = 8
                });
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
