namespace Kubernox.Domain.Entities
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
    }
}
