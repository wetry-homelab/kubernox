using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions.Cluster
{
    public class FetchClustersFailureAction : FailureAction
    {
        public FetchClustersFailureAction(string errorMessage) : base(errorMessage)
        {
        }
    }
}
