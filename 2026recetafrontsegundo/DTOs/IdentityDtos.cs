using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _2026recetafrontsegundo.DTOs
{
    public class CredencialesUsuario
    {
        [Required(ErrorMessage= "el email es requerido")]
        [EmailAddress(ErrorMessage = "el email no es valido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "la contraseña es requerida")]
        [MinLength(8, ErrorMessage = "la contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
    public class RespuestaAutenticacion
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("expiration")]
        public DateTime Expiracion { get; set; }
        [JsonPropertyName("userId")]
        public string UsuarioId { get; set; } = string.Empty;
    }
}
