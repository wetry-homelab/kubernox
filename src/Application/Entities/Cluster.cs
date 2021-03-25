using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class Cluster
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        public int OrderId { get; set; } = 3000;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Domain { get; set; }

        public string Description { get; set; }

        [Required]
        public int ProxmoxNodeId { get; set; }

        [Required]
        public int SshKeyId { get; set; }

        [Required]
        public int Node { get; set; } = 2;

        [Required]
        public int Cpu { get; set; } = 1;

        [Required]
        public int Memory { get; set; } = 512;

        [Required]
        public int Storage { get; set; } = 20;

        [Required]
        public string Ip { get; set; }

        public string KubeConfig { get; set; }

        public string KubeConfigJson { get; set; }

        [Required]
        public string BaseTemplate { get; set; }

        [Required]
        public string User { get; set; } = "root";

        [Required]
        public string State { get; set; } = "Provisionning";

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }

        public virtual ICollection<ClusterNode> Nodes { get; set; }

        public SshKey SshKey { get; set; }
    }
}
