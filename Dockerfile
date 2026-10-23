FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY API.UISIL/API.UISIL.csproj API.UISIL/
COPY API.ENTIDADES/API.ENTIDADES.csproj API.ENTIDADES/
COPY API.LOGICA.NEGOCIO/API.LOGICA.NEGOCIO.csproj API.LOGICA.NEGOCIO/
COPY API.DAPPER/API.DAPPER.csproj API.DAPPER/
RUN dotnet restore API.UISIL/API.UISIL.csproj

COPY . .
RUN dotnet publish API.UISIL/API.UISIL.csproj -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
USER $APP_UID
CMD ["sh", "-c", "exec dotnet API.UISIL.dll --urls http://0.0.0.0:${PORT:-8080}"]
