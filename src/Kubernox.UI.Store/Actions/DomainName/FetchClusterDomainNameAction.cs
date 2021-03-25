using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Actions.DomainName
{
    public class FetchClusterDomainNameAction
    {
        public string ClusterId { get; }

        public FetchClusterDomainNameAction(string clusterId)
        {
            ClusterId = clusterId;
        }
    }
}
