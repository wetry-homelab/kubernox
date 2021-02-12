using Fluxor;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Features
{
    public class DatacenterFeature : Feature<DatacenterState>
    {
        public override string GetName() => "Datacenter";
        protected override DatacenterState GetInitialState() =>
            new DatacenterState(nodes: null, selectedNode: null, isLoading: false, error: null);
    }
}
