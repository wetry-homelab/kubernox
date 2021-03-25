using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class Domain
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string ValidationKey { get; set; }

        public DateTime? ValidationDate { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }
    }
}
