using Infrastructure.Contracts.Request;

namespace Kubernox.UI.Store.Actions.SshKey
{
    public class CreateSshKeyAction
    {
        public SshKeyCreateRequest Request { get; }

        public CreateSshKeyAction(SshKeyCreateRequest request)
        {
            Request = request;
        }
    }
}
