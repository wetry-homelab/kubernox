﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DomainName
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string Name { get; set; }

        [Required]
        public string RootDomain { get; set; }

        [Required]
        public string ValidationKey { get; set; }

        public DateTime? ValidationDate { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }

        public virtual ICollection<SubDomain> SubDomains { get; set; }
    }
}
