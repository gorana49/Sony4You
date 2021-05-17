FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /app
# RUN dotnet dev-certs https
# RUN dotnet dev-certs https --trust
COPY back.csproj .
RUN ["dotnet", "restore"]
COPY . ./
RUN dotnet build "back.csproj" -c Release -o /app/build
RUN dotnet publish "back.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY --from=base /app/publish .
ENTRYPOINT ["dotnet", "back.dll"]
