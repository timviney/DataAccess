# Use the .NET SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy all project files to the container
COPY ["DataAccess/DataAccess.csproj", "./DataAccess/"]
COPY ["DataAccess.Structures/DataAccess.Structures.csproj", "./DataAccess.Structures/"]
COPY ["SudokuProblems/SudokuProblems.csproj", "./DataAccess.Structures/"]

# Restore dependencies for the main project and its references
RUN dotnet restore "DataAccess/DataAccess.csproj"

# Copy the rest of the application source code
COPY . .

# Build the project
RUN dotnet build "DataAccess/DataAccess.csproj" -c Release -o /app/build

# Publish the app to the /app/publish folder with a specific Runtime Identifier (RID)
RUN dotnet publish "DataAccess/DataAccess.csproj" -c Release -o /app/publish -r linux-x64 --self-contained=false

# Download the Lambda Runtime Interface Emulator
RUN curl -Lo /usr/local/bin/aws-lambda-rie \
     https://github.com/aws/aws-lambda-runtime-interface-emulator/releases/latest/download/aws-lambda-rie && \
     chmod +x /usr/local/bin/aws-lambda-rie

# Set the entrypoint to use the emulator with your Lambda handler
ENTRYPOINT [ "/usr/local/bin/aws-lambda-rie", "dotnet", "DataAccess.dll::DataAccess.Function::FunctionHandler" ]

# Create the final image based on the Lambda runtime
FROM public.ecr.aws/lambda/dotnet:8

# Copy the published output from the build stage
COPY --from=build /app/publish/* ${LAMBDA_TASK_ROOT}

# Set the CMD to your handler (could also be done as a parameter override outside of the Dockerfile)
CMD [ "DataAccess::DataAccess.Function::FunctionHandler" ]