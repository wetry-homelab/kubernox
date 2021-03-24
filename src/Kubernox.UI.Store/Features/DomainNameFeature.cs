using Fluxor;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Features
{
    public class DomainNameFeature : Feature<DomainNameState>
    {
        public override string GetName() => "DomainName";
        protected override DomainNameState GetInitialState() =>
            new DomainNameState(domainName: null, domainNames: null, isLoading: false, error: null);
    }
}
