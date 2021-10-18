using Fluxor;
using Kubernox.UI.Store.Actions.Template;
using Kubernox.UI.Store.States;
using System.Linq;

namespace Kubernox.UI.Store.Reducers
{
    public static class TemplateReducer
    {
        [ReducerMethod]
        public static TemplateState ReduceFetchTemplateAction(TemplateState state, FetchTemplateAction _) =>
           new TemplateState(null, true, null);

        [ReducerMethod]
        public static TemplateState ReduceFetchTemplateSuccessAction(TemplateState state, FetchTemplateSuccessAction action) =>
            new TemplateState(action.Templates.ToArray(), false, null);

        [ReducerMethod]
        public static TemplateState ReduceFetchTemplateFailureAction(TemplateState state, FetchTemplateFailureAction action) =>
            new TemplateState(null, false, action.ErrorMessage);

    }
}
