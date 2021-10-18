using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Datacenter;
using Kubernox.UI.Store.States;
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


        [EffectMethod]
        public async Task HandleSelectNodeAction(SelectDatacenterNodeAction action, IDispatcher dispatcher)
        {
            try
            {
                var selectedNode = await datacenterService.GetDatacenterNodeAsync(action.NodeId);
                dispatcher.Dispatch(new SelectDatacenterNodeSuccessAction(selectedNode));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new SelectDatacenterNodeFailureAction(e.Message));
            }
        }
    }
}
