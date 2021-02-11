namespace Kubernox.UI.Store.Actions.Cluster
{
    public class CreateClusterFailureAction : FailureAction
    {
        public CreateClusterFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
