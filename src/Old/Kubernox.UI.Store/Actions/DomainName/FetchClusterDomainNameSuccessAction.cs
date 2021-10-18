using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.Actions.DomainName
{
    public class FetchClusterDomainNameSuccessAction
    {
        public ClusterDomainItemResponse[] DomainNames { get; }

        public FetchClusterDomainNameSuccessAction(ClusterDomainItemResponse[] domainNames)
        {
            DomainNames = domainNames;
        }
    }
}
