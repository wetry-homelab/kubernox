using Fluxor;

using Kubernox.Shared.Interfaces;
using Kubernox.WebUi.States.Actions;

namespace Kubernox.WebUi.States.Effects
{
    public class ProjectEffects
    {
        private readonly IProjectClient projectClient;

        public ProjectEffects(IProjectClient projectClient)
        {
            this.projectClient = projectClient;
        }

        [EffectMethod]
        public async Task HandleFetchProjectActionEffect(FetchProjectAction _, IDispatcher dispatcher)
        {
            var response = await projectClient.GetProjectsAsync();
            dispatcher.Dispatch(new FetchProjectActionSuccess(response));
        }
    }
}
