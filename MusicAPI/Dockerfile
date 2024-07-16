#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000	
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MusicAPI/MusicAPI.csproj", "MusicAPI/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "MusicAPI/MusicAPI.csproj"
COPY . .
RUN dotnet tool install --global dotnet-ef --version 7.0.20


WORKDIR "/src/MusicAPI"
RUN dotnet build "MusicAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicAPI.dll"]