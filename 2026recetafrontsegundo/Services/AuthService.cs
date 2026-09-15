namespace _2026recetafrontsegundo.Services
{
    public interface IAuthService
    {
        Task<RespuestaAutenticacion> Login(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion> Register(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion> renovarToken();
        Task logout();
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private const string endpoint = "api/Cuentas";
        public AuthService(HttpClient httpClient, ItokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<RespuestaAutenticacion> Login(CredencialesUsuario credencialesUsuario)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{endpoint}/Login", credencialesUsuario);
                if (ResponseCachingExtensions.IsSuccessStatus)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();

                    if (respuesta != null)
                    {
                        await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
                        return respuesta;
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"error en login: {error}");
                }

            }
        }

    }

}
namespace _2026recetafrontsegundo.Services
{
    public interface IAuthService
    {
        Task<RespuestaAutenticacion> Login(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion> Register(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion> renovarToken();
        Task logout();
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private const string endpoint = "api/Cuentas";
        public AuthService(HttpClient httpClient, ItokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<RespuestaAutenticacion> Login(CredencialesUsuario credencialesUsuario)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{endpoint}/Login", credencialesUsuario);
                if (ResponseCachingExtensions.IsSuccessStatus)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();

                    if (respuesta != null)
                    {
                        await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
                        return respuesta;
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"error en login: {error}");
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception in Login: {ex.Message}");
                return null;
            }
        }
    }
}
           