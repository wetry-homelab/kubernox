using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions.Cluster
{
    public class FetchClusterFailureAction : FailureAction
    {
        public FetchClusterFailureAction(string errorMessage) : base(errorMessage)
        {
        }
    }
}
