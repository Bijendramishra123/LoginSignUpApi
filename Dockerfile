# Use the official ASP.NET Core SDK image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Ramayan-gita-app.csproj", "./"]
RUN dotnet restore "Ramayan-gita-app.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "Ramayan-gita-app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ramayan-gita-app.dll"]
