using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class DomainNameState : BaseState
    {
        public DomainItemResponse[] DomainNames { get; }
        public ClusterDomainItemResponse[] ClusterDomainNames { get; }
        public DomainItemResponse DomainName { get; }

        public DomainNameState(DomainItemResponse[] domainNames, 
            DomainItemResponse domainName, bool isLoading, string error)
            : base(isLoading, error)
        {
            DomainNames = domainNames;
            DomainName = domainName;
        }

        public DomainNameState(ClusterDomainItemResponse[] clusterDomainNames, bool isLoading, string error)
            : base(isLoading, error)
        {
            ClusterDomainNames = clusterDomainNames;
        }
    }
}
