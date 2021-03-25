using Application.Messages;
using Application.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQueueService
    {
        Task OnQueueMessageInit(Action<string> processMessage);
        void QueueClusterCreation(ClusterMessage message);
        void QueueClusterUpdate(ClusterUpdateMessage message);
        Task OnQueueDeleteMessageInit(Action<string> processMessage);
        void QueueClusterDelete(ClusterMessage message);
    }
}
