namespace Kubernox.Domain.Entities
{
    public class Node
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ClusterConfigurationId { get; set; }

        public string NodeId{ get; set; }

        public long? MaxCpu { get; set; }

        public long? MaxMemory { get; set; }

        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }

        public DateTime? DeleteAt { get; set; }
        public string? DeleteBy { get; set; }

        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
