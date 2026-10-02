# 2026recetafrontsegundo

Breve frontend Blazor para la gestión de autenticación y demostración de componentes.

## Descripción

Proyecto Blazor (Server interactivo) que incluye un servicio de autenticación (AuthService) y modelos básicos para login/registro. Se proporciona la lógica del cliente para comunicarse con una API que expone los endpoints de cuentas.

## Requisitos

- .NET 10 SDK
- Visual Studio 2026 (o `dotnet` CLI)

## Endpoints esperados (API)

El frontend espera que la API exponga los siguientes endpoints:

- POST `api/Cuentas/Login` -> devuelve RespuestaAutenticacion (Token, Expiracion)
- POST `api/Cuentas/Register` -> devuelve RespuestaAutenticacion
- POST `api/Cuentas/RenovarToken` -> devuelve RespuestaAutenticacion

## Archivos importantes añadidos

- `Services/AuthService.cs` — implementación del cliente de autenticación.
- `Services/ITokenService.cs` — interfaz para gestión de token (guardar/obtener/borrar).
- `Models/AuthenticationModels.cs` — modelos `CredencialesUsuario` y `RespuestaAutenticacion`.
- `Components/Pages/Login.razor` — componente de login (marcado simplificado).

## Configuración recomendada (DI)

Registrar los servicios en `Program.cs`. Ejemplo:

```csharp
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient(); // configurar BaseAddress según la API
```

`TokenService` no está implementado en el repositorio; crear una implementación que use almacenamiento protegido (ProtectedLocalStorage / ProtectedSessionStorage) o una alternativa según el hosting.

## Ejecutar

Desde Visual Studio: abrir la solución y ejecutar.
Desde CLI:

```
dotnet build
dotnet run --project 2026recetafrontsegundo/2026recetafrontsegundo.csproj
```

## Notas

- El proyecto compila correctamente después de los cambios recientes.
- Para que el login funcione en ejecución real, implemente `ITokenService` y configure la URL base del HttpClient apuntando a la API.
