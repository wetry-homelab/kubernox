using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Contracts.Request
{
    public class DomainLinkingRequestContract
    {
        [Required]
        public string ClusterId { get; set; }

        public bool LinkRoot { get; set; }

        public string SubDomain { get; set; }

        public string Resolver { get; set; }

        public string CertificateFile { get; set; }

        public string KeyFile { get; set; }
    }
}
