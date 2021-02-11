using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class SshKeyState : BaseState
    {
        public SshKeyResponse[] SshKeys { get; }

        public SshKeyState(SshKeyResponse[] sshKeys, bool isLoading, string error)
            : base(isLoading, error)

        {
            SshKeys = sshKeys;
        }
    }
}
