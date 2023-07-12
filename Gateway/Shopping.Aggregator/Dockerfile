#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Gateway/Shopping.Aggregator/Shopping.Aggregator.csproj", "Gateway/Shopping.Aggregator/"]
COPY ["GeneralBlocks/Common.Logging/Common.Logging.csproj", "GeneralBlocks/Common.Logging/"]
RUN dotnet restore "Gateway/Shopping.Aggregator/Shopping.Aggregator.csproj"
COPY . .
WORKDIR "/src/Gateway/Shopping.Aggregator"
RUN dotnet build "Shopping.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Aggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Aggregator.dll"]