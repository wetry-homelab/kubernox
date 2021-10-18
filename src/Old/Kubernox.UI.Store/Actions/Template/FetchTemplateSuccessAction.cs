using Infrastructure.Contracts.Response;
using System.Collections.Generic;

namespace Kubernox.UI.Store.Actions.Template
{
    public class FetchTemplateSuccessAction
    {
        public FetchTemplateSuccessAction(IEnumerable<TemplateResponse> templates) =>
                Templates = templates;

        public IEnumerable<TemplateResponse> Templates { get; }
    }
}
