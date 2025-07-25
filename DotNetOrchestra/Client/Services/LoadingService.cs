namespace DotNetOrchestra.Client.Services
{
    public class LoadingService
    {
        public event Action? OnStateChanged;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnStateChanged?.Invoke();
                }
            }
        }

        public void Show() => IsLoading = true;
        public void Hide() => IsLoading = false;
    }
}
