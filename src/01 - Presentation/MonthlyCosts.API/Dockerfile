FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/01 - Presentation/MonthlyCosts.API/MonthlyCosts.API.csproj", "src/01 - Presentation/MonthlyCosts.API/"]
COPY ["src/04 - Infrastructure/MonthlyCosts.Infra.IoC/MonthlyCosts.Infra.IoC.csproj", "src/04 - Infrastructure/MonthlyCosts.Infra.IoC/"]
COPY ["src/02 - Application/MonthlyCost.Application/MonthlyCost.Application.csproj", "src/02 - Application/MonthlyCost.Application/"]
COPY ["src/03 - Domain/MonthlyCosts.Domain.Core/MonthlyCosts.Domain.Core.csproj", "src/03 - Domain/MonthlyCosts.Domain.Core/"]
COPY ["src/03 - Domain/MonthlyCosts.Domain/MonthlyCosts.Domain.csproj", "src/03 - Domain/MonthlyCosts.Domain/"]
COPY ["src/03 - Domain/MonthlyCosts.Domain.Services/MonthlyCosts.Domain.Services.csproj", "src/03 - Domain/MonthlyCosts.Domain.Services/"]
COPY ["src/04 - Infrastructure/MonthlyCosts.Data.NoSql/MonthlyCosts.Infra.Data.MongoDb.csproj", "src/04 - Infrastructure/MonthlyCosts.Data.NoSql/"]
COPY ["src/04 - Infrastructure/MonthlyCosts.Infra.Bus/MonthlyCosts.Infra.Bus.csproj", "src/04 - Infrastructure/MonthlyCosts.Infra.Bus/"]
COPY ["src/04 - Infrastructure/MonthlyCosts.Infra.Data.SqlServer/MonthlyCosts.Infra.Data.SqlServer.csproj", "src/04 - Infrastructure/MonthlyCosts.Infra.Data.SqlServer/"]
RUN dotnet restore "./src/01 - Presentation/MonthlyCosts.API/./MonthlyCosts.API.csproj"
COPY . .
WORKDIR "/src/src/01 - Presentation/MonthlyCosts.API"
RUN dotnet build "./MonthlyCosts.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MonthlyCosts.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonthlyCosts.API.dll"]