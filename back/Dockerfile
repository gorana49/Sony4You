#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
COPY back.csproj .
RUN ["dotnet", "restore"]
COPY . ./

# FROM base AS build
RUN dotnet build "back.csproj" -c Release -o /app/build

# FROM build AS publish
RUN dotnet publish "back.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
# WORKDIR /app
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "back.dll"]