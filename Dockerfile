# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /risk-api

# copy csproj and restore as distinct layers
COPY risk-api/*.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY risk-api/. ./
RUN dotnet restore # Restore again because bin and obj folder collide
RUN dotnet publish -c release -o app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /source
COPY --from=build /risk-api/app ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "risk-api.dll"]