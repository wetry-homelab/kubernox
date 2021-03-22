using System;

namespace Infrastructure.Contracts.Response
{
    public class DomainNameItemResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string RootDomain { get; set; }

        public string ValidationKey { get; set; }

        public DateTime? ValidationDate { get; set; }

        public string SubDomainCount { get; set; }
    }
}
