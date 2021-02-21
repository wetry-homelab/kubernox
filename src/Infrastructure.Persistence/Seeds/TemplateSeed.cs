using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Seeds
{
    public class TemplateSeed
    {
        public static List<Template> Templates = new List<Template>()
        {
            new Template()
                {
                    BaseTemplate = "k3s-template",
                    CpuCount = 1,
                    DiskSpace = 20,
                    MemoryCount = 1024,
                    Name = "Small",
                    Type = "k3s"
                },
            new Template()
                {
                    BaseTemplate = "k3s-template",
                    CpuCount = 1,
                    DiskSpace = 30,
                    MemoryCount = 2048,
                    Name = "Medium",
                    Type = "k3s"
                },
            new Template()
                {
                    BaseTemplate = "k3s-template",
                    CpuCount = 2,
                    DiskSpace = 40,
                    MemoryCount = 4096,
                    Name = "Large",
                    Type = "k3s"
                },
            new Template()
                {
                    BaseTemplate = "k3s-template",
                    CpuCount = 4,
                    DiskSpace = 50,
                    MemoryCount = 4096,
                    Name = "XLarge",
                    Type = "k3s"
                },
            new Template()
                {
                    BaseTemplate = "k3s-template",
                    CpuCount = 4,
                    DiskSpace = 80,
                    MemoryCount = 6144,
                    Name = "XXLarge",
                    Type = "k3s"
                },
            new Template()
                {
                    BaseTemplate = "k3s-template",
                    CpuCount = 2,
                    DiskSpace = 200,
                    MemoryCount = 4096,
                    Name = "Storage",
                    Type = "k3s"
                }
        };

        public static void GenerateBaseTemplateSeeds(ServiceDbContext serviceDbContext)
        {
            if (!serviceDbContext.Template.Any())
            {
                serviceDbContext.Template.AddRange(Templates);
                serviceDbContext.SaveChanges();
            }
        }
    }
}
