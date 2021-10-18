using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Actions.DomainName
{
    public class FetchDomainNameFailureAction : FailureAction
    {
        public FetchDomainNameFailureAction(string errorMessage) : base(errorMessage)
        {
        }
    }
}
