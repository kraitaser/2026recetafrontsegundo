using System.Net;
using System.Net.Http.Json;
using _2026recetafrontsegundo.DTOs;

namespace _2026recetafrontsegundo.Services
{
    public interface IAuthService
    {
        Task<RespuestaAutenticacion?> Login(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion?> Register(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion?> RenovarToken();
        Task logout();
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly string endpoint;

        public AuthService(
            HttpClient httpClient,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            endpoint = $"{(Environment.GetEnvironmentVariable("API_URL")
                ?? configuration["Api:BaseUrl"])?.TrimEnd('/')
                ?? throw new InvalidOperationException("No se ha configurado Api:BaseUrl.")}/api/Accounts";
        }

        private async Task<RespuestaAutenticacion?> SendAuthenticationRequestAsync(
            HttpRequestMessage request)
        {
            using var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var details = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"La API respondió {(int)response.StatusCode} ({response.ReasonPhrase}). {details}",
                    null,
                    response.StatusCode);
            }

            var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();
            if (respuesta is not null)
            {
                await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
            }

            return respuesta;
        }

        public async Task<RespuestaAutenticacion?> Login(CredencialesUsuario credencialesUsuario)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{endpoint}/login")
            {
                Content = JsonContent.Create(credencialesUsuario)
            };

            return await SendAuthenticationRequestAsync(request);
        }

        public async Task<RespuestaAutenticacion?> Register(CredencialesUsuario credenciales)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{endpoint}/register")
            {
                Content = JsonContent.Create(credenciales)
            };

            return await SendAuthenticationRequestAsync(request);
        }
        public async Task<RespuestaAutenticacion?> RenovarToken()
        {
            try
            {
                var token = await tokenService.ObtenerToken();
                if (string.IsNullOrEmpty(token))
                    return null;
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();
                    if (respuesta != null)
                    {
                        await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
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
        //public async Task<respuestaAutenticacion?> isLogged()
        //{
        //    try
        //    {
        //        var token = await tokenService.ObtenerToken();
        //    }
        //}
        public Task logout()
        {
            throw new NotImplementedException();
        }
    }
}
           