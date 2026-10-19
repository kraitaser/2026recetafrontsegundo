using _2026recetafrontsegundo.Services;

namespace _2026recetafrontsegundo.Handlers
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {

        private readonly ITokenService tokenService;

        public AuthorizationMessageHandler(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await tokenService.ObtenerToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
