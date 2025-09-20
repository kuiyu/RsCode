namespace RsCode.AspNetCore.Plugin
{
    public class ServiceRegistrationProxy
    {
        private readonly List<Action<IServiceCollection>> _registrationActions = new();
        private readonly object _lock = new object();

        public void AddRegistration(Action<IServiceCollection> registrationAction)
        {
            lock (_lock)
            {
                _registrationActions.Add(registrationAction);
            }
        }

        public void ApplyRegistrations(IServiceCollection services)
        {
            lock (_lock)
            {
                foreach (var action in _registrationActions)
                {
                    action(services);
                }
            }
        }

        public void ClearRegistrations()
        {
            lock (_lock)
            {
                _registrationActions.Clear();
            }
        }
    }
}
