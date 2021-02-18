using Fluxor;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Features
{
    public class ClusterFeature : Feature<ClusterState>
    {
        public override string GetName() => "Cluster";
        protected override ClusterState GetInitialState() =>
            new ClusterState(clusters: null, cluster: null, isLoading: false, error: null);
    }
}
