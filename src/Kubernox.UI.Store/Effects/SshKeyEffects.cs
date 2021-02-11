using Fluxor;
using Services;
using Kubernox.UI.Store.Actions.SshKey;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class SshKeyEffects
    {
        private readonly SshKeyService sshKeyService;

        public SshKeyEffects(SshKeyService sshKeyService)
        {
            this.sshKeyService = sshKeyService;
        }

        [EffectMethod]
        public async Task HandleFetchSshKeyAction(FetchSshKeyAction action, IDispatcher dispatcher)
        {
            try
            {
                var sshKeys = await sshKeyService.GetSshKeysAsync();
                dispatcher.Dispatch(new FetchSshKeySuccessAction(sshKeys));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchSshKeyFailureAction(e.Message));
            }
        }

        [EffectMethod]
        public async Task HandleCreateSshKeyAction(CreateSshKeyAction action, IDispatcher dispatcher)
        {
            try
            {
                var sshKeys = await sshKeyService.ImportSshKeysAsync(action.Request);
                if (sshKeys)
                    dispatcher.Dispatch(new FetchSshKeyAction());
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchSshKeyFailureAction(e.Message));
            }
        }

        [EffectMethod]
        public async Task HandleDeleteSshKeyAction(DeleteSshKeyAction action, IDispatcher dispatcher)
        {
            try
            {
                var sshKeys = await sshKeyService.DeleteSshKeysAsync(action.Id);
                if (sshKeys)
                    dispatcher.Dispatch(new FetchSshKeyAction());
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchSshKeyFailureAction(e.Message));
            }
        }
    }
}
