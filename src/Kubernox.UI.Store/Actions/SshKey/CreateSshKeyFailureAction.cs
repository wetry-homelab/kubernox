using System;
using System.Collections.Generic;
using System.Text;

namespace Kubernox.UI.Store.Actions.SshKey
{
    public class CreateSshKeyFailureAction : FailureAction
    {
        public CreateSshKeyFailureAction(string errorMessage) : base(errorMessage)
        {
        }
    }
}
