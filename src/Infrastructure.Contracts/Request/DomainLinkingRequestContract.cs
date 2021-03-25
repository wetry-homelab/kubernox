using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Contracts.Request
{
    public class DomainLinkingRequestContract
    {
        public string ClusterId { get; set; }

        public string DomainNameId { get; set; }

        public string SubDomain { get; set; }

        public string Resolver { get; set; }

        public string CertificateFile { get; set; }

        public string KeyFile { get; set; }
    }
}
