using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class SshKey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Public { get; set; }

        public string Private { get; set; }

        [Required]
        public string Fingerprint { get; set; }

        public string Pem { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }

        public virtual ICollection<Cluster> Clusters { get; set; }

    }
}
