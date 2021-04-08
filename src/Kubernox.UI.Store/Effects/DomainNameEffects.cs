using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.DomainName;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class DomainNameEffects
    {
        private readonly IDomainService domainNameService;

        public DomainNameEffects(IDomainService domainNameService)
        {
            this.domainNameService = domainNameService;
        }

        [EffectMethod]
        public async Task HandleFetchDomainAction(FetchDomainNameAction action, IDispatcher dispatcher)
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

        [EffectMethod]
        public async Task HandleFetchClusterDomainAction(FetchClusterDomainNameAction action, IDispatcher dispatcher)
        {
            try
            {
                var domainNames = await domainNameService.GetDomainsForClusterAsync(action.ClusterId);
                dispatcher.Dispatch(new FetchClusterDomainNameSuccessAction(domainNames));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchClusterDomainNameFailureAction(e.Message));
            }
        }
    }
}
