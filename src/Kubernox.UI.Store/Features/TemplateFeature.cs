using Fluxor;
using Kubernox.UI.Store.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Features
{
    public class TemplateFeature : Feature<TemplateState>
    {
        public override string GetName() => "Template";
        protected override TemplateState GetInitialState() =>
            new TemplateState(templates: null, isLoading: false, error: null);
    }
}
