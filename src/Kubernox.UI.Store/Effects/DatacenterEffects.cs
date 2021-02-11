using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Datacenter;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class DatacenterEffects
    {
        private readonly IDatacenterService datacenterService;

        public DatacenterEffects(IDatacenterService datacenterService)
        {
            this.datacenterService = datacenterService;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(FetchDatacenterAction action, IDispatcher dispatcher)
        {
            try
            {
                var datacenterNodes = await datacenterService.GetDatacentersAsync();
                dispatcher.Dispatch(new FetchDatacenterSuccessAction(datacenterNodes));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchDatacenterFailureAction(e.Message));
            }
        }
    }
}
