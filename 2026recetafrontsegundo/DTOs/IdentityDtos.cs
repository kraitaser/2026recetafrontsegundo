using System.ComponentModel.DataAnnotations;

namespace _2026recetafrontsegundo.DTOs
{
    public class CredencialesUsuario
    {
        [Required(ErrorMessage= "el email es requerido")]
        [EmailAddress(ErrorMessage = "el email no es valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "la contraseña es requerida")]
        [MinLength(8, ErrorMessage = "la contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }
    }
    public class RespuestaAutenticacion
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
        public String UsuarioId { get; set; } = string.Empty;
    }
}
