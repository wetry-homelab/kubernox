using Infrastructure.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions.Cluster
{
    public class FetchClusterSuccessAction
    {
        public FetchClusterSuccessAction(ClusterDetailsResponse cluster)
        {
            Cluster = cluster;
        }

        public ClusterDetailsResponse Cluster { get; }
    }
}
