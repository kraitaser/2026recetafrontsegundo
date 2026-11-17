using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _2026recetafrontsegundo.Models;

public sealed class CategoriaDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("createdAtUtc")]
    public DateTime CreatedAtUtc { get; set; }

    [JsonPropertyName("recipeIds")]
    public List<int> RecipeIds { get; set; } = [];
}

public sealed class IngredienteDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("createdAtUtc")]
    public DateTime CreatedAtUtc { get; set; }
}

public sealed class RecetaDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("createdAtUtc")]
    public DateTime CreatedAtUtc { get; set; }

    [JsonPropertyName("ingredients")]
    public List<RecetaIngredienteDto> Ingredientes { get; set; } = [];

    [JsonPropertyName("categoryIds")]
    public List<int> CategoryIds { get; set; } = [];

    [JsonPropertyName("comments")]
    public List<ComentarioDto> Comentarios { get; set; } = [];
}

public sealed class RecetaIngredienteDto
{
    [JsonPropertyName("ingredientId")]
    public int IngredientId { get; set; }

    [JsonPropertyName("ingredientName")]
    public string IngredientName { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;
}

public sealed class RecetaIngredienteRequest
{
    [JsonPropertyName("ingredientId")]
    public int IngredientId { get; set; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;
}

public sealed class CategoriaRequest
{
    [JsonPropertyName("Name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("recipeIds")]
    public List<int> RecipeIds { get; set; } = [];
}

public sealed class IngredienteRequest
{
    [JsonPropertyName("Name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Descripcion { get; set; }
}

public sealed class RecetaRequest
{
    [JsonPropertyName("Name")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("ingredients")]
    public List<RecetaIngredienteRequest> Ingredientes { get; set; } = [];

    [JsonPropertyName("categoryIds")]
    public List<int> CategoryIds { get; set; } = [];
}

public sealed class ComentarioDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Texto { get; set; } = string.Empty;

    [JsonPropertyName("createdAtUtc")]
    public DateTime CreatedAtUtc { get; set; }

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("userName")]
    public string? UserName { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }
}

public sealed class ComentarioRequest
{
    [Required(ErrorMessage = "El comentario es obligatorio.")]
    [StringLength(1000, ErrorMessage = "El comentario no puede superar los 1000 caracteres.")]
    [JsonPropertyName("text")]
    public string Texto { get; set; } = string.Empty;
}
