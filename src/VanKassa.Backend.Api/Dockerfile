FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy AS base
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
COPY --from=build "src/VanKassa.Backend.Infrastructure/SqlScripts" /app/sql_scripts
RUN apt-get update
RUN apt-get install -y -f libgconf-2-4 gnupg git curl wget ca-certificates libgconf-2-4 && \
    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' && \
    apt-get update && \
    apt-get install -y libappindicator3-1 && \
    apt-get install -y libgdk-pixbuf2.0-0 && \
    apt-get install -y libxss1 && \
    apt-get install -y -f lsb-release google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst && \
    wget https://github.com/webnicer/chrome-downloads/raw/master/x64.deb/google-chrome-stable_79.0.3945.130-1_amd64.deb && \
    dpkg -i ./google-chrome*.deb && \
    rm google-chrome*.deb

ENV chrome_launchOptions_executablePath google-chrome-stable
ENV chrome_launchOptions_args --no-sandbox,--disable-dev-shm-usage

ENTRYPOINT ["dotnet", "VanKassa.Backend.Api.dll"]
