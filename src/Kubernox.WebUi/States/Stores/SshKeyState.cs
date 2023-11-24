using Fluxor;

using Kubernox.Shared;

namespace Kubernox.WebUi.States.Stores
{
    [FeatureState]
    public class SshKeyState
    {
        public bool IsLoading { get; }

        public IEnumerable<SSHKeyItemResponse> SshKeys { get; }

        public SshKeyState()
        {

        }

        public SshKeyState(bool isLoading, IEnumerable<SSHKeyItemResponse> sshKeys)
        {
            IsLoading = isLoading;
            SshKeys = sshKeys;
        }
    }
}
