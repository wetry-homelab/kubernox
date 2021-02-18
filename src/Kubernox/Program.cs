using Kubernox.Services;
using McMaster.Extensions.CommandLineUtils;
using System.Threading;
using System.Threading.Tasks;

namespace Kubernox
{
    class Program
    {
        private static readonly OrchestratorService orchestratorService = new OrchestratorService();

        static Task<int> Main(string[] args)
            => CommandLineApplication.ExecuteAsync<Program>(args);




        [Option(CommandOptionType.SingleOrNoValue, ShortName = "u", Description = "Upgrade application stack", LongName = "upgrade")]
        public bool Upgrade { get; } = false;

        private Task OnExecute()
        {
            var cancellationToken = new CancellationToken();
            return orchestratorService.StartDeploymentAsync(cancellationToken, Upgrade);
        }
    }
}
