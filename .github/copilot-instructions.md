# Copilot Instructions

## Directrices del proyecto
- La API del proyecto expone CRUD en /api/Categories, /api/Ingredients y /api/Recipes: GET/POST en la colección, GET/PUT/DELETE por id y GET por name. Los endpoints usan nombres en inglés y DELETE se documenta como DEL.
- En las páginas Blazor, inyectar ApiClient con @inject y usar los nombres de propiedades definidos por los DTO: los modelos actuales usan Nombre, Cantidad, Categoria y Tiempo, y el request de ingredientes es IngredienteRequest.