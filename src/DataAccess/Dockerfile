# Use the .NET SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy only the project file first to improve build caching
COPY ["DataAccess.csproj", "./"]

# Restore any dependencies
RUN dotnet restore "DataAccess.csproj"

# Copy the rest of the application source code
COPY . .

# Build the project
RUN dotnet build "DataAccess.csproj" -c Release -o /app/build

# Publish the app to the /app/publish folder with a specific Runtime Identifier (RID)
RUN dotnet publish "DataAccess.csproj" -c Release -o /app/publish -r linux-x64 --self-contained=false

# Create the final image based on the Lambda runtime
FROM public.ecr.aws/lambda/dotnet:8

# Copy the published output from the build stage
COPY --from=build /app/publish/* ${LAMBDA_TASK_ROOT}

# Set the CMD to your handler (could also be done as a parameter override outside of the Dockerfile)
CMD [ "DataAccess::DataAccess.Function::DataAccess" ]
