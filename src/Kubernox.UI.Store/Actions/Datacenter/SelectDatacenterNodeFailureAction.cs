using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions.Datacenter
{
    public class SelectDatacenterNodeFailureAction : FailureAction
    {
        public SelectDatacenterNodeFailureAction(string errorMessage) : base(errorMessage)
        {
        }
    }
}
