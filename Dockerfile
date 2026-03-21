FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/BibliotecaApi/BibliotecaApi.csproj src/BibliotecaApi/
COPY tests/BibliotecaApi.Tests/BibliotecaApi.Tests.csproj tests/BibliotecaApi.Tests/

RUN dotnet restore src/BibliotecaApi/BibliotecaApi.csproj

COPY . .

RUN dotnet publish src/BibliotecaApi/BibliotecaApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

COPY src/BibliotecaApi/Biblioteca.db /app/Biblioteca.db

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "BibliotecaApi.dll"]