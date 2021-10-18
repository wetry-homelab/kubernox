using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions
{
    public abstract class FailureAction
    {
        protected FailureAction(string errorMessage) =>
            ErrorMessage = errorMessage;

        public string ErrorMessage { get; }
    }
}
