FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MonthlyCosts.API/MonthlyCosts.API.csproj", "MonthlyCosts.API/"]
RUN dotnet restore "MonthlyCosts.API/MonthlyCosts.API.csproj"
COPY . .
WORKDIR "/src/MonthlyCosts.API"
RUN dotnet build "MonthlyCosts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonthlyCosts.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonthlyCosts.API.dll"]