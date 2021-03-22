using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class DomainNameState : BaseState
    {
        public DomainNameItemResponse[] DomainNames { get; }

        public DomainNameItemResponse DomainName { get; }

        public DomainNameState(DomainNameItemResponse[] domainNames, DomainNameItemResponse domainName, bool isLoading, string error)
            : base(isLoading, error)
        {
            DomainNames = domainNames;
            DomainName = domainName;
        }
    }
}
