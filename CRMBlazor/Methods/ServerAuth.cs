using Microsoft.AspNetCore.Components.Authorization;

namespace CRMBlazor.Methods
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider, IHostEnvironmentAuthenticationStateProvider
    {
        private Task<AuthenticationState> _authenticationStateTask;

        /// <inheritdoc />
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
            => _authenticationStateTask
            ?? throw new InvalidOperationException($"{nameof(GetAuthenticationStateAsync)} was called before {nameof(SetAuthenticationState)}.");

        /// <inheritdoc />
        public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
        {
            _authenticationStateTask = authenticationStateTask ?? throw new ArgumentNullException(nameof(authenticationStateTask));
            NotifyAuthenticationStateChanged(_authenticationStateTask);
        }
    }
}

