#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Genial.CashFlow.Api/Genial.CashFlow.Api.csproj", "Genial.CashFlow.Api/"]
COPY ["Genial.CashFlow.Application/Genial.CashFlow.Application.csproj", "Genial.CashFlow.Application/"]
COPY ["Genial.CashFlow.Application.Dtos/Genial.CashFlow.Application.Dtos.csproj", "Genial.CashFlow.Application.Dtos/"]
COPY ["Genial.CashFlow.Domain/Genial.CashFlow.Domain.csproj", "Genial.CashFlow.Domain/"]
COPY ["Genial.CashFlow.Infrastructure/Genial.CashFlow.Infrastructure.csproj", "Genial.CashFlow.Infrastructure/"]

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