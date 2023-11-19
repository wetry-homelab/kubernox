namespace Kubernox.WebUi.Core
{
    public class CoreFailureAction
    {
        public CoreFailureAction(string errorMessage, bool isError)
        {
            ErrorMessage = errorMessage;
            IsError = isError;
        }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
    }
}
