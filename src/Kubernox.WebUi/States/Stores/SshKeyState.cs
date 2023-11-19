using Fluxor;

using Kubernox.Shared.Contracts.Response;

namespace Kubernox.WebUi.States.Stores
{
    [FeatureState]
    public class SshKeyState
    {
        public bool IsLoading { get; }

        public IEnumerable<SshKeyItemResponse> SshKeys { get; }

        public SshKeyState()
        {

        }

        public SshKeyState(bool isLoading, IEnumerable<SshKeyItemResponse> sshKeys)
        {
            IsLoading = isLoading;
            SshKeys = sshKeys;
        }
    }
}
