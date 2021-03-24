using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.Actions.DomainName
{
    public class FetchDomainNameSuccessAction
    {
        public DomainNameItemResponse[] DomainNames { get; }

        public FetchDomainNameSuccessAction(DomainNameItemResponse[] domainNames)
        {
            DomainNames = domainNames;
        }
    }
}
