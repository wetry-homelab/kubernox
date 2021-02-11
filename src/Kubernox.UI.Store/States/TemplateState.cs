using Infrastructure.Contracts.Response;

namespace Kubernox.UI.Store.States
{
    public class TemplateState : BaseState
    {
        public TemplateResponse[] Templates { get; }

        public TemplateState(TemplateResponse[] templates, bool isLoading, string error)
            : base(isLoading, error)
        {
            Templates = templates;
        }
    }
}
