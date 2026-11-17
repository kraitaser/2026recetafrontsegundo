using System.Net.Http.Headers;
using System.Net.Http.Json;
using _2026recetafrontsegundo.Models;

namespace _2026recetafrontsegundo.Services;

public sealed class ApiClient
{
    private readonly HttpClient httpClient;
    private readonly ITokenService tokenService;

    public ApiClient(HttpClient httpClient, ITokenService tokenService)
    {
        this.httpClient = httpClient;
        this.tokenService = tokenService;
    }

    private async Task<HttpRequestMessage> CreateRequestAsync(
        HttpMethod method,
        string url)
    {
        var request = new HttpRequestMessage(method, url);
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var token = await tokenService.ObtenerToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return request;
    }

    private async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var details = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            try
            {
                await tokenService.EliminarToken();
            }
            catch (InvalidOperationException)
            {
            }
        }

        throw new HttpRequestException(
            $"La API respondió {(int)response.StatusCode} ({response.ReasonPhrase}). {details}",
            null,
            response.StatusCode);
    }

    public async Task<List<CategoriaDto>> GetCategoriasAsync()
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            "/api/Categories");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<List<CategoriaDto>>()
            ?? [];
    }

    public async Task<CategoriaDto?> GetCategoriaAsync(int id)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Categories/{id}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<CategoriaDto>();
    }

    public async Task<CategoriaDto?> GetCategoriaByNameAsync(string name)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Categories/{Uri.EscapeDataString(name)}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<CategoriaDto>();
    }

    public async Task<CategoriaDto?> CreateCategoriaAsync(
        CategoriaRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Post,
            "/api/Categories");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<CategoriaDto>();
    }

    public async Task UpdateCategoriaAsync(
        int id,
        CategoriaRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Put,
            $"/api/Categories/{id}");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);
    }

    public async Task DeleteCategoriaAsync(int id)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Delete,
            $"/api/Categories/{id}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);
    }

    public async Task<List<IngredienteDto>> GetIngredientesAsync()
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            "/api/Ingredients");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<List<IngredienteDto>>()
            ?? [];
    }

    public async Task<IngredienteDto?> GetIngredienteAsync(int id)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Ingredients/{id}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<IngredienteDto>();
    }

    public async Task<IngredienteDto?> GetIngredienteByNameAsync(string name)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Ingredients/{Uri.EscapeDataString(name)}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<IngredienteDto>();
    }

    public async Task<IngredienteDto?> CreateIngredienteAsync(
        IngredienteRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Post,
            "/api/Ingredients");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<IngredienteDto>();
    }

    public async Task UpdateIngredienteAsync(
        int id,
        IngredienteRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Put,
            $"/api/Ingredients/{id}");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);
    }

    public async Task DeleteIngredienteAsync(int id)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Delete,
            $"/api/Ingredients/{id}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);
    }

    public async Task<List<RecetaDto>> GetRecetasAsync()
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            "/api/Recipes");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<List<RecetaDto>>()
            ?? [];
    }

    public async Task<RecetaDto?> GetRecetaAsync(int id)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Recipes/{id}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<RecetaDto>();
    }

    public async Task<RecetaDto?> GetRecetaByNameAsync(string name)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Recipes/{Uri.EscapeDataString(name)}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<RecetaDto>();
    }

    public async Task<RecetaDto?> CreateRecetaAsync(
        RecetaRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Post,
            "/api/Recipes");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<RecetaDto>();
    }

    public async Task UpdateRecetaAsync(
        int id,
        RecetaRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Put,
            $"/api/Recipes/{id}");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);
    }

    public async Task DeleteRecetaAsync(int id)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Delete,
            $"/api/Recipes/{id}");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);
    }

    public async Task<List<ComentarioDto>> GetComentariosAsync(int recetaId)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Get,
            $"/api/Recipes/{recetaId}/comments");

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<List<ComentarioDto>>()
            ?? [];
    }

    public async Task<ComentarioDto?> CreateComentarioAsync(
        int recetaId,
        ComentarioRequest model)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Post,
            $"/api/Recipes/{recetaId}/comments");

        request.Content = JsonContent.Create(model);

        using var response = await httpClient.SendAsync(request);
        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<ComentarioDto>();
    }
}