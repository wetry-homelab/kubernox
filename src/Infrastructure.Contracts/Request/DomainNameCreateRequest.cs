namespace Infrastructure.Contracts.Request
{
    public class DomainNameCreateRequest
    {
        public string Name { get; set; }

        public string RootDomain { get; set; }
    }
}
