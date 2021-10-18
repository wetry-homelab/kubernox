using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class DomainState : BaseState
    {
        public DomainItemResponse[] Domains { get; }
        public ClusterDomainItemResponse[] ClusterDomains { get; }
        public DomainItemResponse Domain { get; }

        public DomainState(DomainItemResponse[] domains, ClusterDomainItemResponse[] clusterDomains, DomainItemResponse domain, bool isLoading, string error)
            : base(isLoading, error)
        {
            Domains = domains;
            ClusterDomains = clusterDomains;
            Domain = domain;
        }
    }
}
