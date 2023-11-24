using Fluxor;

using Kubernox.Shared;
using Kubernox.WebUi.States.Actions;

namespace Kubernox.WebUi.States.Effects
{
    public class HostStateEffects
    {
        private readonly IKubernoxClient kubernoxClient;

        public HostStateEffects(IKubernoxClient kubernoxClient)
        {
            this.kubernoxClient = kubernoxClient;
        }

        [EffectMethod]
        public async Task HandleFetchHostActionEffect(FetchHostAction _, IDispatcher dispatcher)
        {
            var response = await kubernoxClient.ListHostsAsync();
            dispatcher.Dispatch(new FetchHostActionSuccess(response));
        }
    }
}
