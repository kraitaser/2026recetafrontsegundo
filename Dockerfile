FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY 2026recetafrontsegundo/2026recetafrontsegundo.csproj 2026recetafrontsegundo/
RUN dotnet restore 2026recetafrontsegundo/2026recetafrontsegundo.csproj

COPY . .
WORKDIR /src/2026recetafrontsegundo
RUN dotnet publish 2026recetafrontsegundo.csproj --configuration Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "2026recetafrontsegundo.dll"]