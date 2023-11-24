using Kubernox.Application.Interfaces;
using Kubernox.Application.Models;

using Pulumi;
using Pulumi.ProxmoxVE.VM.Inputs;

namespace Kubernox.Application.Services
{
    public class PulumiService : ICodeAsInfrastructureService
    {
        public Task<bool> Deploy(DeploymentModel deploymentModel)
        {
            throw new NotImplementedException();
        }

        private Pulumi.ProxmoxVE.VM.VirtualMachine MapDeploymentToVm(DeploymentModel deploymentModel)
        {
            var vm = new Pulumi.ProxmoxVE.VM.VirtualMachine(deploymentModel.Name, new Pulumi.ProxmoxVE.VM.VirtualMachineArgs()
            {
                Agent = new VirtualMachineAgentArgs()
                {
                    Enabled = deploymentModel.Agent
                },
                Clone = new VirtualMachineCloneArgs()
                {
                    VmId = deploymentModel.TemplateId,
                    NodeName = deploymentModel.TemplateNode,
                    Full = true
                },
                Description = deploymentModel.Description,
                Disks = deploymentModel.DiskSizes.Select(s =>
                    new VirtualMachineDiskArgs()
                    {
                        Interface = s.Key,
                        Size = s.Value,
                        FileFormat = "raw"
                    }
                ).ToList(),
                Cpu = new VirtualMachineCpuArgs()
                {
                    Cores = deploymentModel.Cpu,
                    Sockets = deploymentModel.Socket,
                },
                Initialization = new VirtualMachineInitializationArgs()
                {
                    Type = "nocloud",
                    UserAccount = new VirtualMachineInitializationUserAccountArgs()
                    {
                        Password = "xxQb6FVes",
                        Username = "tech",
                        Keys = new InputList<string>()
                        {

                        }
                    },
                    Interface = "ide2",
                    IpConfigs = new InputList<VirtualMachineInitializationIpConfigArgs>()
                    {
                        new VirtualMachineInitializationIpConfigArgs()
                        {
                            Ipv4 = new VirtualMachineInitializationIpConfigIpv4Args()
                            {
                                Address = "192.168.50.10/28",
                                Gateway = "192.168.50.1"
                            }
                        }
                    }
                },
                Memory = new VirtualMachineMemoryArgs()
                {
                    Dedicated = deploymentModel.Memory,
                    Shared = 2048
                },
                Name = deploymentModel.Name,
                NodeName = deploymentModel.NodeName,
                VmId = deploymentModel.Id
            });

            return vm;
        }
    }
}
