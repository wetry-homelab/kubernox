namespace Kubernox.Domain.Entities
{
    public class HostConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ip { get; set; }

        public string ApiToken { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }

        public DateTime? DeleteAt { get; set; }
        public string? DeleteBy { get; set; }    
        
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
