using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SubDomain
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        public string Sub { get; set; }

        [Required]
        public string ChallengeType { get; set; }

        [Required]
        public string Challenge { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string FullChain { get; set; }

        public string PrivateKey { get; set; }

        [Required]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }

        public string DomainNameId { get; set; }

        public DomainName DomainName { get; set; }
    }
}
