using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Cluster;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class ClusterEffects
    {
        private readonly IClusterService clusterService;

        public ClusterEffects(IClusterService clusterService)
        {
            this.clusterService = clusterService;
        }

        [EffectMethod]
        public async Task HandleFetchClusterAction(FetchClusterAction action, IDispatcher dispatcher)
        {
            try
            {
                var clusters = await clusterService.GetClustersAsync();
                dispatcher.Dispatch(new FetchClusterSuccessAction(clusters));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchClusterFailureAction(e.Message));
            }
        }

        [EffectMethod]
        public async Task HandleCreateClusterAction(CreateClusterAction action, IDispatcher dispatcher)
        {
            try
            {
                var clusterReactionResult = await clusterService.CreateClustersAsync(action.Request);

                if (clusterReactionResult)
                    dispatcher.Dispatch(new FetchClusterAction());

            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new CreateClusterFailureAction(e.Message));
            }
        }
    }
}
