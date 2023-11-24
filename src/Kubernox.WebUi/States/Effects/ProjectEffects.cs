using Fluxor;
using Kubernox.Shared;
using Kubernox.WebUi.States.Actions;

namespace Kubernox.WebUi.States.Effects
{
    public class ProjectEffects
    {
        private readonly IKubernoxClient kubernoxClient;

        public ProjectEffects(IKubernoxClient kubernoxClient)
        {
            this.kubernoxClient = kubernoxClient;
        }

        [EffectMethod]
        public async Task HandleFetchProjectActionEffect(FetchProjectAction _, IDispatcher dispatcher)
        {
            var response = await kubernoxClient.ListProjectsAsync();
            dispatcher.Dispatch(new FetchProjectActionSuccess(response));
        }
    }
}
