namespace Kubernox.UI.Store.States
{
    public abstract class BaseState
    {
        public BaseState(bool isLoading, string? currentErrorMessage) =>
                    (IsLoading, CurrentErrorMessage) = (isLoading, currentErrorMessage);

        public bool IsLoading { get; }

        public string? CurrentErrorMessage { get; }

        public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(CurrentErrorMessage);
    }
}
