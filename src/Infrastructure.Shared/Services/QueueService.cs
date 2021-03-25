using Application.Interfaces;
using Application.Messages;
using Application.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConnection connectionQueue;
        private readonly ILogger<QueueService> logger;

        public QueueService(ILogger<QueueService> logger, IConfiguration configuration)
        {
            this.logger = logger;

            ConnectionFactory factory = new ConnectionFactory()
            {
                UserName = configuration["RabbitMq:User"],
                Password = configuration["RabbitMq:Password"],
                HostName = configuration["RabbitMq:HostName"],
                Port = int.Parse(configuration["RabbitMq:Port"]),
                VirtualHost = configuration["RabbitMq:VirtualHost"]
            };

            this.connectionQueue = factory.CreateConnection();
        }

        public async Task OnQueueDeleteMessageInit(Action<string> processMessage)
        {
            using (var channel = connectionQueue.CreateModel())
            {
                channel.QueueDeclare(queue: "k3s_queue_delete", true, false, false, null);
                channel.QueueBind(queue: "k3s_queue_delete",
                                  exchange: "amq.fanout",
                                  routingKey: "");

                Console.WriteLine("[*] Waiting for queue => Delete Cluster.");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    logger.LogInformation("Message receive");
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    processMessage.Invoke(message);
                };

                channel.BasicConsume(queue: "k3s_queue_delete", autoAck: true, consumer: consumer);
                await Task.Delay(-1);
            }
        }

        public async Task OnQueueMessageInit(Action<string> processMessage)
        {
            using (var channel = connectionQueue.CreateModel())
            {
                channel.QueueDeclare(queue: "k3s_queue_result", true, false, false, null);
                channel.QueueBind(queue: "k3s_queue_result",
                                  exchange: "amq.fanout",
                                  routingKey: "");

                Console.WriteLine("[*] Waiting for queue => Cluster creation.");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    logger.LogInformation("Message receive");
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    processMessage.Invoke(message);
                };

                channel.BasicConsume(queue: "k3s_queue_result", autoAck: true, consumer: consumer);
                await Task.Delay(-1);
            }
        }

        public void QueueClusterCreation(ClusterMessage message)
        {
            var routingKey = "k3s_queue";

            PublishMessage(message, routingKey);
        }

        public void QueueClusterUpdate(ClusterUpdateMessage message)
        {
            var routingKey = "k3s_queue";

            PublishMessage(message, routingKey);
        }

        public void QueueClusterDelete(ClusterMessage message)
        {
            var routingKey = "k3s_queue_delete";
            PublishMessage(message,  routingKey);
        }

        private void PublishMessage(object data, string routingKey)
        {
            try
            {
                IModel channel = connectionQueue.CreateModel();
                channel.QueueDeclare(queue: routingKey, false, false, false, null);

                byte[] messageBodyBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
                channel.BasicPublish(exchange: "", routingKey: routingKey, body: messageBodyBytes, basicProperties: null);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Exception on publishing message in queue.");
            }
        }
    }
}
