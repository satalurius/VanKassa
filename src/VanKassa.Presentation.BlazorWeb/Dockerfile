FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN apt-get update -y
RUN apt-get upgrade -y
RUN apt-get install -y python3
RUN dotnet workload install wasm-tools
RUN dotnet restore "VanKassa.Presentation.BlazorWeb/VanKassa.Presentation.BlazorWeb.csproj" 
WORKDIR "/src/VanKassa.Presentation.BlazorWeb"
RUN dotnet build "VanKassa.Presentation.BlazorWeb.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "VanKassa.Presentation.BlazorWeb.csproj" -c Release -o /app/publish

FROM nginx:alpine AS nginx
WORKDIR /var/www/web
COPY --from=publish /app/publish/wwwroot .
COPY --from=publish /src/VanKassa.Presentation.BlazorWeb/nginx.conf /etc/nginx/nginx.conf
EXPOSE 3000
