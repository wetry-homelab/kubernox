using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions.Datacenter
{
    public class FetchDatacenterFailureAction : FailureAction
    {
        public FetchDatacenterFailureAction(string errorMessage) : base(errorMessage)
        {
        }
    }
}
