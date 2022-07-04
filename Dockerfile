# Estagio de constru��o
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY . ./

# Realiza a publica��o do projeto .NET
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Estagio de servi�o
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]