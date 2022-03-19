using System;

namespace Kubernox.Infrastructure.Infrastructure.Entities
{
    public class Host
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string User { get; set; }
        public string Ip { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
