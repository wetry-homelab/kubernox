using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.DomainName;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class DomainNameEffects
    {
        private readonly IDomainNameService domainNameService;

        public DomainNameEffects(IDomainNameService domainNameService)
        {
            this.domainNameService = domainNameService;
        }

        [EffectMethod]
        public async Task HandleFetchSshKeyAction(FetchDomainNameAction action, IDispatcher dispatcher)
        {
            try
            {
                var domainNames = await domainNameService.GetDomainsAsync();
                dispatcher.Dispatch(new FetchDomainNameSuccessAction(domainNames));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchDomainNameFailureAction(e.Message));
            }
        }
    }
}
