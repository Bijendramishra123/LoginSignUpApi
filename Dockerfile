# STEP 1: Use .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore dependencies
COPY . .
RUN dotnet restore

# Build and publish the app
RUN dotnet publish -c Release -o /app/publish

# STEP 2: Use .NET Runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 for the app
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "Ramayan-gita-app.dll"]
