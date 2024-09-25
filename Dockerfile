# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["src/DataAccess/DataAccess.csproj", "src/DataAccess/"]
RUN dotnet restore "src/DataAccess/DataAccess.csproj"

# Copy the entire source code to the container
COPY src/ .

# Set the working directory to the project folder
WORKDIR "/src/DataAccess"

# Build the project
RUN dotnet build "DataAccess.csproj" -c Release -o /app/build

# Publish the app to the /app/publish folder with a specific Runtime Identifier (RID)
FROM build AS publish
WORKDIR "/src/DataAccess"
RUN dotnet publish "DataAccess.csproj" -c Release -o /app/publish -r linux-x64 --self-contained=false

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataAccess.dll"]
