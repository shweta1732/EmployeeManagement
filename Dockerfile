# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution file and project files
COPY ["EmployeeManagementSystem.slnx", "EmployeeManagementSystem.slnx"]
COPY ["EmployeeManagement.API/EmployeeManagement.API.csproj", "EmployeeManagement.API/"]
COPY ["EmployeeManagement.Application/EmployeeManagement.Application.csproj", "EmployeeManagement.Application/"]
COPY ["EmployeeManagement.Domain/EmployeeManagement.Domain.csproj", "EmployeeManagement.Domain/"]
COPY ["EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj", "EmployeeManagement.Infrastructure/"]
COPY ["EmployeeManagement.Tests/EmployeeManagement.Tests.csproj", "EmployeeManagement.Tests/"]

# Restore dependencies
RUN dotnet restore "EmployeeManagementSystem.slnx"

# Copy source code
COPY . .

# Build the application
RUN dotnet build "EmployeeManagementSystem.slnx" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "EmployeeManagement.API/EmployeeManagement.API.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:5000/health || exit 1

ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]
