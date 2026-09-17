using _2026recetafrontsegundo.DTOs;

namespace _2026recetafrontsegundo.Services
{
    public interface IAuthService
    {
        Task<RespuestaAutenticacion> Login(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion> Register(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion> RenovarToken();
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
                if (ResponseCachingExtensions.IsSuccessStatusCode)
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
        public async Task<RespuestaAutenticacion?> Register(CredencialesUsuario credenciales)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{endpoint}/Register", credenciales);
                //todo revisar el enpoin en el baken
                if (response.IsSuccessStatus)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();
                    if(respuesta != null)
                    {
                        await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
                        return respuesta;
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error al registrar:{ex.Message}");
                return null;
            }
        }
        public async Task<RespuestaAutenticacion?> RenovarToken()
        {
            try
            {
                var token = await tokenService.ObtenerToken();
                if (string.IsNullOrEmpty(token))
                    return null;
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"{endpoint}/RenovarToken");
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();
                    if(response != null)
                    {
                        await tokenService.GuardarToken(respuesta!.Token, respuesta.Expiracion);
                        return respuesta;
                    }
                }
                return null;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"error al renovar token:{ex.Message}");
                return null;
            }
        }

        public Task logout()
        {
            throw new NotImplementedException();
        }
    }
}
           