namespace Kubernox.Application.Models
{
    public class DeploymentModel
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? NodeName { get; set; }

        public string? Description { get; set; }

        public string? Hostname { get; set; }

        public int Cpu { get; set; }

        public int Socket { get; set; }

        public int Memory { get; set; }

        public bool Agent { get; set; } = true;

        public int TemplateId { get; set; }

        public string TemplateNode { get; set; }

        public Dictionary<string, int> DiskSizes { get; set; } = new Dictionary<string, int>() { { "scsi0", 20 } };
    }
}
