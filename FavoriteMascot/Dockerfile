#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FavoriteMascot/FavoriteMascot.csproj", "FavoriteMascot/"]
RUN dotnet restore "FavoriteMascot/FavoriteMascot.csproj"
COPY . .
WORKDIR "/src/FavoriteMascot"
RUN dotnet build "FavoriteMascot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoriteMascot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoriteMascot.dll"]