using Fluxor;
using Kubernox.WebUi.States.Actions;
using Kubernox.WebUi.States.Stores;

namespace Kubernox.WebUi.States.Reducers
{
    public static class ProjectReducers
    {
        [ReducerMethod]
        public static ProjectState ReduceFetchProjectAction(ProjectState projectState, FetchProjectAction _)
            => new ProjectState(projectState.Projects, true);

        [ReducerMethod]
        public static ProjectState ReduceFetchProjectActionSuccess(ProjectState _, FetchProjectActionSuccess action)
            => new ProjectState(action.Projects, false);

        [ReducerMethod]
        public static ProjectState ReduceFetchProjectActionFailure(ProjectState projectState, FetchProjectActionFailure _)
            => new ProjectState(projectState.Projects, false);
    }
}
