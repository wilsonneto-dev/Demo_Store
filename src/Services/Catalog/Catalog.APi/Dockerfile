﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./src/Services/Catalog/Catalog.APi/Catalog.APi.csproj", "./src/Services/Catalog/Catalog.APi/"]
RUN dotnet restore "./src/Services/Catalog/Catalog.APi/Catalog.APi.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Catalog.APi"
RUN dotnet build "Catalog.APi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.APi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.APi.dll"]
