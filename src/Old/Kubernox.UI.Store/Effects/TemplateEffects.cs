using Fluxor;
using Kubernox.UI.Services.Interfaces;
using Kubernox.UI.Store.Actions.Template;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class TemplateEffects
    {
        private readonly ITemplateService templateService;

        public TemplateEffects(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        [EffectMethod]
        public async Task HandleFetchSshKeyAction(FetchTemplateAction _, IDispatcher dispatcher)
        {
            try
            {
                var templates = await templateService.GetTemplatesAsync();
                dispatcher.Dispatch(new FetchTemplateSuccessAction(templates));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new FetchTemplateFailureAction(e.Message));
            }
        }
    }
}
