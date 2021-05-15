FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /app
COPY back.csproj .
RUN ["dotnet", "restore"]
RUN dotnet dev-certs https
COPY . ./
RUN dotnet build "back.csproj" -c Release -o /app/build
RUN dotnet publish "back.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "back.dll"]
