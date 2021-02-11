using Fluxor;
using Services;
using Kubernox.UI.Store.Actions.Template;
using System;
using System.Threading.Tasks;

namespace Kubernox.UI.Store.Effects
{
    public class TemplateEffects
    {
        private readonly TemplateService templateService;

        public TemplateEffects(TemplateService templateService)
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
