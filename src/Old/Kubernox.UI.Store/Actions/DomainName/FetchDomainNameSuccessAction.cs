using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.Actions.DomainName
{
    public class FetchDomainNameSuccessAction
    {
        public DomainItemResponse[] DomainNames { get; }

        public FetchDomainNameSuccessAction(DomainItemResponse[] domainNames)
        {
            DomainNames = domainNames;
        }
    }
}
