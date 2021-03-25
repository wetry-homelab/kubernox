using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Response
{
    public class ClusterDomainItemResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string RootDomainId { get; set; }

        public string ClusterId { get; set; }

        public string Resolver { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeleteAt { get; set; }
    }
}
