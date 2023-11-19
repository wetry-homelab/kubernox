namespace Kubernox.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Identifier { get; set; }

        public string? Description { get; set; }
    }
}
