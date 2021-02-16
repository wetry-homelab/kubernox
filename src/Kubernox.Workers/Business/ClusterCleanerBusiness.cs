using Domain.Entities;
using Microsoft.Extensions.Configuration;
using ProxmoxVEAPI.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernox.Workers.Business
{
    public class ClusterCleanerBusiness
    {
        private readonly IConfiguration configuration;
        private readonly QemuClient qemuClient;

        public ClusterCleanerBusiness(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConfigureClient.Initialise(configuration["Proxmox:Uri"], configuration["Proxmox:Token"]);
            qemuClient = new QemuClient();
        }

        public async Task StartCleanCleanCluster(Cluster cluster)
        {
            var statusStopped = false;

            do
            {
            } while (!statusStopped);
        }
    }
}
