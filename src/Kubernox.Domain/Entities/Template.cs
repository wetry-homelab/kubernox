namespace Kubernox.Domain.Entities
{
    public class Template
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long VmId { get; set; }

        public Guid HostConfigurationId { get; set; }

        public Guid NodeId { get; set; }
    }
}
