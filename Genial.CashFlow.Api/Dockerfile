#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Genial.CashFlow.Api/Genial.CashFlow.Api.csproj", "Genial.CashFlow.Api/"]
COPY ["Genial.CashFlow.Business/Genial.CashFlow.Business.csproj", "Genial.CashFlow.Business/"]
COPY ["Genial.CashFlow.Data/Genial.CashFlow.Data.csproj", "Genial.CashFlow.Data/"]
COPY ["Genial.CashFlow.Entities/Genial.CashFlow.Entities.csproj", "Genial.CashFlow.Entities/"]
COPY ["Genial.CashFlow.Dtos/Genial.CashFlow.Dtos.csproj", "Genial.CashFlow.Dtos/"]
COPY ["Genial.Framework/Genial.Framework.csproj", "Genial.Framework/"]
RUN dotnet restore "Genial.CashFlow.Api/Genial.CashFlow.Api.csproj"
COPY . .
WORKDIR "/src/Genial.CashFlow.Api"
RUN dotnet build "Genial.CashFlow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Genial.CashFlow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN apt-get update 
RUN apt-get --yes install curl
HEALTHCHECK --interval=10s --timeout=20s --retries=10 \
  CMD curl "http://localhost/healthz" --fail --show-error --silent || exit 1

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genial.CashFlow.Api.dll"]