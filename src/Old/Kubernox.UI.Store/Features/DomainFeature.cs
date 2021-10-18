using Fluxor;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Features
{
    public class DomainFeature : Feature<DomainState>
    {
        public override string GetName() => "Domain";
        protected override DomainState GetInitialState() =>
            new DomainState(domains: null, domain: null, clusterDomains:null, isLoading: false, error: null);
    }
}