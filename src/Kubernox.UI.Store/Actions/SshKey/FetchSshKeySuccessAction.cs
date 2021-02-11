using Infrastructure.Contracts.Response;
using System.Collections.Generic;

namespace Kubernox.UI.Store.Actions.SshKey
{
    public class FetchSshKeySuccessAction
    {
        public FetchSshKeySuccessAction(IEnumerable<SshKeyResponse> keys) =>
                Keys = keys;

        public IEnumerable<SshKeyResponse> Keys { get; }
    }
}
