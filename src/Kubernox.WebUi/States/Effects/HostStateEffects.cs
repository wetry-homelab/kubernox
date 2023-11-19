using Fluxor;

using Kubernox.Shared.Interfaces;
using Kubernox.WebUi.States.Actions;

namespace Kubernox.WebUi.States.Effects
{
    public class HostStateEffects
    {
        private readonly IHostClient hostClient;

        public HostStateEffects(IHostClient hostClient)
        {
            this.hostClient = hostClient;
        }

        [EffectMethod]
        public async Task HandleFetchHostActionEffect(FetchHostAction _, IDispatcher dispatcher)
        {
            var response = await hostClient.GetHostsAsync();
            dispatcher.Dispatch(new FetchHostActionSuccess(response));
        }
    }
}
