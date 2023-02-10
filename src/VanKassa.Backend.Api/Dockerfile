FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY "VanKassa.Backend.Api/VanKassa.Backend.Api.csproj" "VanKassa.Backend.Api/"
COPY "VanKassa.Backend.Core/VanKassa.Backend.Core.csproj" "VanKassa.Backend.Core/"
COPY "VanKassa.Backend.Infrastructure/VanKassa.Backend.Infrastructure.csproj" "VanKassa.Backend.Infrastructure/"
COPY "VanKassa.Domain/VanKassa.Domain.csproj" "VanKassa.Domain/"
RUN dotnet restore "VanKassa.Backend.Api/VanKassa.Backend.Api.csproj"
COPY . .
WORKDIR "/src/VanKassa.Backend.Api"
RUN dotnet build "VanKassa.Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VanKassa.Backend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VanKassa.Backend.Api.dll"]
