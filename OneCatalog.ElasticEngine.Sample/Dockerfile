#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OneCatalog.ElasticEngine.Sample/OneCatalog.ElasticEngine.Sample.csproj", "OneCatalog.ElasticEngine.Sample/"]
RUN dotnet restore "OneCatalog.ElasticEngine.Sample/OneCatalog.ElasticEngine.Sample.csproj"
COPY . .
WORKDIR "/src/OneCatalog.ElasticEngine.Sample"
RUN dotnet build "OneCatalog.ElasticEngine.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OneCatalog.ElasticEngine.Sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OneCatalog.ElasticEngine.Sample.dll"]