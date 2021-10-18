FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Kubernox.Host/Kubernox.Host.csproj", "src/Kubernox.Host/"]
COPY ["src/Kubernox.UI/Kubernox.UI.csproj", "src/Kubernox.UI/"]
COPY ["src/Kubernox.UI.Store/Kubernox.UI.Store.csproj", "src/Kubernox.UI.Store/"]
COPY ["src/Kubernox.UI.Services/Kubernox.UI.Services.csproj", "src/Kubernox.UI.Services/"]
COPY ["src/Infrastructure.Contracts/Infrastructure.Contracts.csproj", "src/Infrastructure.Contracts/"]
COPY ["src/Infrastructure.Validators/Infrastructure.Validators.csproj", "src/Infrastructure.Validators/"]
RUN dotnet restore "src/Kubernox.Host/Kubernox.Host.csproj"
COPY . .
WORKDIR "/src/src/Kubernox.Host"
RUN dotnet build "Kubernox.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kubernox.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kubernox.Host.dll"]