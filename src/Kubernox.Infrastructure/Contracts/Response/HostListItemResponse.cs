using System;

namespace Kubernox.Infrastructure.Contracts.Response
{
    public class HostListItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Ip { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
