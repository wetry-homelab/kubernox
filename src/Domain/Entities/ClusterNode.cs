using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ClusterNode
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        public string Name { get; set; }

        public string ClusterId { get; set; }

        public Cluster Cluster { get; set; }

        [Required]
        public int OrderId { get; set; } = 3000;

        [Required]
        public string Ip { get; set; }

        [Required]
        public string State { get; set; } = "NotReady";

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }
    }
}
