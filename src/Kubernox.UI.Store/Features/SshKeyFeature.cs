using Fluxor;
using Kubernox.UI.Store.States;

namespace Kubernox.UI.Store.Features
{
    class SshKeyFeature : Feature<SshKeyState>
    {
        public override string GetName() => "SshKey";
        protected override SshKeyState GetInitialState() =>
            new SshKeyState(sshKeys: null, isLoading: false, error: null);
    }
}
