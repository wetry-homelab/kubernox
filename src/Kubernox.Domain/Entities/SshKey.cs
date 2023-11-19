namespace Kubernox.Domain.Entities
{
    public class SshKey
    {
        public Guid Id { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }

        public DateTime? DeleteAt { get; set; }
        public string? DeleteBy { get; set; }

        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
