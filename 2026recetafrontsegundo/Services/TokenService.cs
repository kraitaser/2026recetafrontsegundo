using Microsoft.JSInterop;

namespace _2026recetafrontsegundo.Services
{
    public interface ITokenService
    {
        Task GuardarToken(String token, DateTime expiracion);
        Task<String> ObtenerToken();
        Task<DateTime?> ObtenerExpiracion();
        Task<bool> EstaAutenticado();
        Task EliminarToken();
    }
    public class tokenService : ITokenService
    {
        private readonly IJSRuntime jsRuntime;
        private const string TOKEN_KEY = "authToken";
        private const string EXPIRATION_KEY = "tokenExpiracion";

        public tokenService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }
        public async Task EliminarToken()
        {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", EXPIRATION_KEY);
        }
        public async Task<bool> EstaAutenticado()
        {
            var token = await ObtenerToken();
            return !string.IsNullOrEmpty(token);

        }
        public async Task GuardarToken(string token, DateTime expiracion)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", EXPIRATION_KEY, expiracion.ToString("o"));
        }
        public async Task<DateTime> ObtenerExpiracion()
        {
            try
            {
                var ExpiracionString = await jsRuntime.InvokeAsync<string>("localStorage.getItem", EXPIRATION_KEY);
                
                if(string.IsNullOrEmpty(ExpiracionString)) 
                    return null;
                if (DateTime.TryParse(ExpiracionString, out var expiracion))
                    return expiracion;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<String> ObtenerToken()
        {
            try
            {
                var token = jsRuntime.InvokeAsync<string?>("localStorage.getItem", TOKEN_KEY);
                if (string.IsNullOrEmpty(token))
                    return null;
                var expiracion = await ObtenerExpiracion();
                if(expiracion.hasValue && expiracion.Value < DateTime.UtcNow)
                {
                    await EliminarToken();
                    return null;
                }
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}