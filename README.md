# Employee Management System

A modern, production-ready full-stack application for managing employees with a user-friendly interface and secure API backend.

🔗 **Live Demo:** Coming soon  
📖 **Full Documentation:** See [ARCHITECTURE.md](ARCHITECTURE.md)  
📋 **Implementation Plan:** See [PLAN.md](PLAN.md)

---

## Table of Contents

- [Project Overview](#project-overview)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Features](#features)
- [Folder Structure](#folder-structure)
- [Prerequisites](#prerequisites)
- [Database Setup](#database-setup)
- [Backend Run Instructions](#backend-run-instructions)
- [Frontend Run Instructions](#frontend-run-instructions)
- [Docker Setup](#docker-setup)
- [API Documentation](#api-documentation)
- [Authentication](#authentication)
- [Development Workflow](#development-workflow)
- [Testing](#testing)
- [Deployment](#deployment)
- [Troubleshooting](#troubleshooting)
- [Future Improvements](#future-improvements)
- [Contributing](#contributing)
- [License](#license)

---

## Project Overview

The **Employee Management System** is a comprehensive web application designed to streamline employee data management with the following capabilities:

- **User Authentication:** Secure JWT-based authentication with role-based access control
- **Employee CRUD:** Create, read, update, and delete employee records with validation
- **Dashboard:** Visual overview of workforce metrics and statistics
- **Responsive UI:** Modern, mobile-friendly interface built with Angular Material and Bootstrap
- **API-First Design:** RESTful API with comprehensive Swagger documentation
- **Docker Ready:** Containerized for development and production deployment
- **CI/CD Pipeline:** Automated testing and deployment via GitHub Actions

**Target Users:**
- HR Managers: Manage employee records and access employee information
- Administrators: Configure system settings and user access
- Employees: View own profile information

---

## Technology Stack

### Backend

| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | .NET | 10.0 (LTS) |
| **Language** | C# | Latest |
| **Database** | SQL Server | 2022 |
| **ORM** | Entity Framework Core | 10.0.4 |
| **Authentication** | JWT Bearer | Standard |
| **Validation** | FluentValidation | 11.3.1 |
| **Mapping** | AutoMapper | 12.0.1 |
| **API Documentation** | Swagger/Swashbuckle | 10.1.5 |
| **Testing** | xUnit + Moq | Latest |

### Frontend

| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | Angular | 19 |
| **Language** | TypeScript | 5.6 |
| **Styling** | Bootstrap + Angular Material | 5.3 / 19 |
| **HTTP Client** | Angular HttpClient | Built-in |
| **Reactive Programming** | RxJS | 7.8+ |
| **Forms** | Reactive Forms | Built-in |
| **Build Tool** | Angular CLI | 19 |

### DevOps & Tools

| Tool | Purpose | Version |
|------|---------|---------|
| **Docker** | Containerization | Latest |
| **Docker Compose** | Multi-container orchestration | 3.8+ |
| **GitHub Actions** | CI/CD Pipeline | Latest |
| **nginx** | Reverse proxy / Frontend server | Alpine |
| **Node.js** | Frontend build environment | 22 |

---

## Architecture

The application follows a **layered clean architecture** pattern:

```
┌─────────────────────────────────────────────────┐
│         Angular 19 SPA (Frontend)               │
│  - Components, Services, Guards, Interceptors   │
│  - Reactive Forms & Validation                  │
│  - Angular Material & Bootstrap UI              │
└────────────────┬────────────────────────────────┘
                 │
         HTTP/REST + JWT
                 │
┌────────────────▼────────────────────────────────┐
│    .NET 10 API (Backend)                        │
├─────────────────────────────────────────────────┤
│  Presentation Layer: Controllers                │
│  Application Layer: Services, Validation        │
│  Domain Layer: Entities, Interfaces             │
│  Infrastructure Layer: Repositories, DbContext  │
└────────────────┬────────────────────────────────┘
                 │
         EF Core / SQL
                 │
┌────────────────▼────────────────────────────────┐
│      SQL Server 2022 (Database)                 │
│  - Employees Table                              │
│  - Users Table (Authentication)                 │
└─────────────────────────────────────────────────┘
```

**Key Design Patterns:**
- Repository Pattern for data access abstraction
- Dependency Injection for loose coupling
- Service-Oriented Architecture for business logic
- DTO Pattern for API contracts
- AutoMapper for object transformation

For detailed architecture documentation, see [ARCHITECTURE.md](ARCHITECTURE.md).

---

## Features

### ✅ Implemented Features

#### Authentication & Security
- ✅ JWT-based authentication (user login)
- ✅ Role-based access control (Admin, User roles)
- ✅ Secure password hashing
- ✅ Authorization guards on routes
- ✅ CORS configuration for frontend integration

#### Employee Management
- ✅ Create new employee records
- ✅ View employee list with pagination and sorting
- ✅ View individual employee details
- ✅ Update employee information
- ✅ Delete employee records
- ✅ Input validation on create/update forms

#### User Interface
- ✅ Responsive login page
- ✅ Administrative dashboard with metrics
- ✅ Employee data grid (Material table)
- ✅ Add/Edit employee modal forms
- ✅ Error handling and user notifications
- ✅ Mobile-friendly design

#### Developer Experience
- ✅ Swagger API documentation
- ✅ Auto-generated API specs
- ✅ Unit tests for services and controllers
- ✅ Code comments and documentation
- ✅ GitHub Actions CI/CD pipeline

#### Deployment
- ✅ Docker containerization (backend & frontend)
- ✅ Docker Compose for local development
- ✅ Multi-stage builds for optimization
- ✅ Health checks for container orchestration
- ✅ Environment-based configuration

### 🚀 Planned Features

- Search and advanced filtering for employee data
- Excel/CSV export of employee records
- Attendance tracking
- Performance management module
- Payroll integration
- Email notifications
- File uploads (employee documents)
- Audit log tracking
- Two-factor authentication (2FA)
- Real-time notifications (SignalR)
- Mobile app (React Native)

---

## Folder Structure

```
EmployeeManagement/
├── .github/
│   └── workflows/
│       └── build.yml                      # CI/CD Pipeline
├── EmployeeManagement.API/
│   ├── Controllers/
│   │   ├── AuthController.cs              # Authentication endpoints
│   │   └── EmployeeController.cs          # Employee CRUD endpoints
│   ├── Services/
│   │   └── JwtTokenService.cs             # JWT token generation
│   ├── Mapping/
│   │   └── MappingProfile.cs              # AutoMapper configuration
│   ├── appsettings.json                   # Configuration (production)
│   ├── appsettings.Development.json       # Configuration (development)
│   ├── Program.cs                         # Application entry point
│   ├── EmployeeManagement.API.csproj      # Project file
│   └── EmployeeManagement.API.http        # HTTP test requests
├── EmployeeManagement.Application/
│   ├── DTOs/
│   │   ├── LoginDto.cs                    # Login request DTO
│   │   ├── AuthResponseDto.cs             # Auth response DTO
│   │   ├── EmployeeCreateDto.cs           # Create employee DTO
│   │   ├── EmployeeReadDto.cs             # Read employee DTO
│   │   └── EmployeeUpdateDto.cs           # Update employee DTO
│   ├── Interfaces/
│   │   ├── IAuthService.cs                # Authentication service contract
│   │   ├── IEmployeeService.cs            # Employee service contract
│   │   ├── IRepository.cs                 # Generic repository contract
│   │   ├── ITokenService.cs               # Token service contract
│   │   └── IUserRepository.cs             # User repository contract
│   ├── Services/
│   │   ├── AuthService.cs                 # Authentication business logic
│   │   └── EmployeeService.cs             # Employee business logic
│   ├── Validators/
│   │   ├── EmployeeCreateDtoValidator.cs  # Create validation rules
│   │   ├── EmployeeReadDtoValidator.cs    # Read validation rules
│   │   ├── EmployeeUpdateDtoValidator.cs  # Update validation rules
│   │   └── EmployeeValidator.cs           # Common validation rules
│   └── EmployeeManagement.Application.csproj
├── EmployeeManagement.Domain/
│   ├── Entities/
│   │   ├── Employee.cs                    # Employee entity
│   │   └── User.cs                        # User entity
│   ├── Class1.cs
│   └── EmployeeManagement.Domain.csproj
├── EmployeeManagement.Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs        # EF Core DbContext
│   ├── Repositories/
│   │   ├── Repository.cs                  # Generic repository implementation
│   │   └── UserRepository.cs              # User repository implementation
│   └── EmployeeManagement.Infrastructure.csproj
├── EmployeeManagement.Tests/
│   ├── EmployeeControllerTests.cs         # Controller unit tests
│   ├── EmployeeServiceTests.cs            # Service unit tests
│   └── EmployeeManagement.Tests.csproj
├── employee-management-ui/
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   │   ├── sidebar/               # Navigation sidebar
│   │   │   │   └── header/                # Application header
│   │   │   ├── pages/
│   │   │   │   ├── login/                 # Login page
│   │   │   │   ├── dashboard/             # Dashboard page
│   │   │   │   ├── employees/             # Employee list page
│   │   │   │   ├── employee-detail/       # Employee details page
│   │   │   │   ├── add-employee/          # Add employee page
│   │   │   │   └── edit-employee/         # Edit employee page
│   │   │   ├── services/
│   │   │   │   ├── auth.service.ts        # Authentication service
│   │   │   │   ├── employee.service.ts    # Employee API service
│   │   │   │   └── storage.service.ts     # Local storage service
│   │   │   ├── guards/
│   │   │   │   └── auth.guard.ts          # Route protection guard
│   │   │   ├── interceptors/
│   │   │   │   └── jwt.interceptor.ts     # JWT token interceptor
│   │   │   ├── models/
│   │   │   │   ├── employee.model.ts      # Employee interface
│   │   │   │   └── user.model.ts          # User interface
│   │   │   ├── app.component.ts           # Root component
│   │   │   ├── app-routing.module.ts      # Route definitions
│   │   │   └── app.module.ts              # App module
│   │   ├── environments/
│   │   │   ├── environment.ts             # Development config
│   │   │   ├── environment.prod.ts        # Production config
│   │   │   └── environment.docker.ts      # Docker config
│   │   ├── index.html                     # HTML entry point
│   │   ├── main.ts                        # Application bootstrap
│   │   ├── styles.css                     # Global styles
│   │   └── polyfills.ts                   # Polyfills
│   ├── Dockerfile                         # Frontend container definition
│   ├── nginx.conf                         # nginx configuration
│   ├── angular.json                       # Angular CLI configuration
│   ├── package.json                       # npm dependencies
│   ├── tsconfig.json                      # TypeScript configuration
│   └── tsconfig.app.json                  # TypeScript app configuration
├── Dockerfile                             # Backend container definition
├── docker-compose.yml                     # Docker Compose orchestration
├── EmployeeManagement.slnx                # Solution file
├── ARCHITECTURE.md                        # Architecture documentation
├── PLAN.md                                # Implementation plan
├── README.md                              # This file
└── .gitignore                             # Git ignore rules
```

---

## Prerequisites

### System Requirements

- **OS:** Windows 10+, macOS 11+, or Linux (Ubuntu 20.04+)
- **RAM:** Minimum 4GB (8GB recommended)
- **Disk Space:** 5GB minimum

### Required Software

#### For Backend Development:
- [.NET 10 SDK](https://dotnet.microsoft.com/download) - Download and install
- [SQL Server 2022](https://www.microsoft.com/sql-server/sql-server-downloads) or [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-express)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) (optional but recommended)

#### For Frontend Development:
- [Node.js 22](https://nodejs.org/) (includes npm)
- [Angular CLI](https://angular.io/cli) - Install globally: `npm install -g @angular/cli@19`
- Code Editor: [Visual Studio Code](https://code.visualstudio.com/) (recommended)

#### For Docker Development:
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (Windows/macOS)
- [Docker Engine](https://docs.docker.com/engine/install/) (Linux)
- [Docker Compose](https://docs.docker.com/compose/) (included with Docker Desktop)

#### Optional Tools:
- [Postman](https://www.postman.com/) - API testing
- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) - Full-featured IDE
- [Git](https://git-scm.com/) - Version control

### Verify Installation

```bash
# Check .NET installation
dotnet --version
# Should output: 10.x.x

# Check Node.js installation
node --version npm --version
# Should output: v22.x.x and npm version

# Check Docker installation
docker --version docker-compose --version
# Should output: Docker version 24.x.x and 2.x.x

# Check Angular CLI installation
ng version
# Should output: Angular 19.x.x
```

---

## Database Setup

### Option 1: Local SQL Server (Development)

#### Using SQL Server Management Studio (SSMS)

1. **Connect to SQL Server:**
   - Open SSMS
   - Server name: `localhost` or `.`
   - Authentication: Windows Authentication or SQL Server Authentication
   - Click Connect

2. **Create Database:**
   ```sql
   CREATE DATABASE EmployeeManagement;
   USE EmployeeManagement;
   ```

3. **Create Tables:**
   ```sql
   -- Users Table
   CREATE TABLE Users (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Username NVARCHAR(100) NOT NULL UNIQUE,
       PasswordHash NVARCHAR(MAX) NOT NULL,
       Email NVARCHAR(100) NOT NULL,
       Role NVARCHAR(50) NOT NULL,
       IsActive BIT NOT NULL DEFAULT 1,
       CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
   );

   -- Employees Table
   CREATE TABLE Employees (
       Id INT PRIMARY KEY IDENTITY(1,1),
       FirstName NVARCHAR(100) NOT NULL,
       LastName NVARCHAR(100) NOT NULL,
       Email NVARCHAR(100) NOT NULL,
       PhoneNumber NVARCHAR(20),
       Department NVARCHAR(100),
       Salary DECIMAL(10, 2),
       HireDate DATETIME,
       IsActive BIT NOT NULL DEFAULT 1,
       CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
       UpdatedAt DATETIME
   );

   -- Seed Sample Users
   INSERT INTO Users (Username, PasswordHash, Email, Role, IsActive)
   VALUES 
   ('admin', '$2a$11$...', 'admin@example.com', 'Admin', 1),
   ('user', '$2a$11$...', 'user@example.com', 'User', 1);

   -- Seed Sample Employees
   INSERT INTO Employees (FirstName, LastName, Email, PhoneNumber, Department, Salary, HireDate, IsActive)
   VALUES 
   ('John', 'Doe', 'john.doe@example.com', '123-456-7890', 'IT', 75000, '2023-01-15', 1),
   ('Jane', 'Smith', 'jane.smith@example.com', '098-765-4321', 'HR', 65000, '2023-02-01', 1);
   ```

#### Using Entity Framework Core Migrations (Recommended)

1. **Install EF Core Tools:**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Apply Migrations from Solution Root:**
   ```bash
   cd EmployeeManagement.API
   dotnet ef database update --context ApplicationDbContext
   ```

3. **Verify Database Creation:**
   - Open SSMS and verify `EmployeeManagement` database and tables exist

### Option 2: Docker SQL Server (Development/Testing)

See [Docker Setup](#docker-setup) section for container-based database setup.

### Database Connection String

Update [EmployeeManagement.API/appsettings.Development.json](EmployeeManagement.API/appsettings.Development.json):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EmployeeManagement;User Id=sa;Password=YourPassword;TrustServerCertificate=true;"
  }
}
```

**Note:** Replace `YourPassword` with your actual SQL Server password.

---

## Backend Run Instructions

### Prerequisites
- .NET 10 SDK installed
- SQL Server running with EmployeeManagement database
- Connection string configured in appsettings.Development.json

### Step-by-Step Guide

1. **Navigate to Backend Directory:**
   ```bash
   cd EmployeeManagement.API
   ```

2. **Restore NuGet Dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the Solution:**
   ```bash
   dotnet build
   ```

4. **Run the Application:**
   ```bash
   dotnet run
   ```

   **Output should show:**
   ```
   info: Microsoft.Hosting.Lifetime[14]
            Now listening on: http://localhost:5000
   info: Microsoft.Hosting.Lifetime[0]
            Application started. Press Ctrl+C to shut down.
   ```

5. **Access the API:**
   - Swagger UI: http://localhost:5000/swagger
   - API Base: http://localhost:5000/api

### Troubleshooting Backend

**Issue:** "Invalid connection string"
- **Solution:** Verify SQL Server is running and connection string in appsettings.json is correct

**Issue:** "Database does not exist"
- **Solution:** Run migrations: `dotnet ef database update`

**Issue:** "Port 5000 already in use"
- **Solution:** Change port in `launchSettings.json` or modify environment: `set ASPNETCORE_URLS=http://localhost:5001`

**Issue:** "NuGet restore fails"
- **Solution:** Clear NuGet cache: `dotnet nuget locals all --clear` then retry

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test EmployeeManagement.Tests

# Run with verbose output
dotnet test --verbosity normal

# Generate coverage report
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

---

## Frontend Run Instructions

### Prerequisites
- Node.js 22+ installed
- npm installed (comes with Node.js)
- Angular CLI installed globally

### Step-by-Step Guide

1. **Navigate to Frontend Directory:**
   ```bash
   cd employee-management-ui
   ```

2. **Install npm Dependencies:**
   ```bash
   npm install
   ```
   This creates a `node_modules/` folder with all required packages (~500MB).

3. **Start Development Server:**
   ```bash
   npm start
   # or
   ng serve
   ```

   **Output should show:**
   ```
   ✔ Compiled successfully.
   ✔ Build at: 2026-03-12T10:30:00.000Z

   Application bundle generation v19.0.0
   Build at: 2026-03-12T10:30:05.000Z
   ```

4. **Access the Application:**
   - Open browser: http://localhost:4200
   - Page auto-reloads on code changes

5. **Login with Credentials:**
   - **Admin Account:**
     - Username: `admin`
     - Password: `admin123`
   - **Regular User:**
     - Username: `user`
     - Password: `user123`

### Development Commands

```bash
# Start dev server with live reload
npm start

# Build for production
npm run build

# Run unit tests
npm test

# Run end-to-end tests
npm run e2e

# Lint code
npm run lint

# Build and serve production build locally
npm run build
npx http-server -p 8080 -c-1 dist/employee-management-ui/browser
```

### Troubleshooting Frontend

**Issue:** "Node modules not found"
- **Solution:** Delete node_modules folder and package-lock.json, then run `npm install` again

**Issue:** "Port 4200 already in use"
- **Solution:** Use different port: `ng serve --port 4300`

**Issue:** API connection fails
- **Solution:** Verify backend is running on http://localhost:5000 and CORS is enabled

**Issue:** "Cannot find module '@angular/material'"
- **Solution:** Ensure Angular Material is installed: `npm install @angular/material`

---

## Docker Setup

### Quick Start with Docker Compose

**Easiest way to run entire application!**

1. **Build Containers:**
   ```bash
   docker-compose build
   ```

2. **Start Services:**
   ```bash
   docker-compose up
   ```
   This starts:
   - SQL Server on port 1433
   - .NET API on port 5000
   - Angular UI on port 80

3. **Access Applications:**
   - **Frontend:** http://localhost
   - **Backend API:** http://localhost:5000
   - **Swagger Docs:** http://localhost:5000/swagger
   - **Database:** localhost:1433 (user: `sa`, password: `Admin@123456`)

4. **Stop Services:**
   ```bash
   docker-compose down
   ```

### Individual Docker Commands

#### Build Backend Image
```bash
docker build -t employee-api:latest .
```

#### Run Backend Container
```bash
docker run -d \
  -p 5000:5000 \
  -e ASPNETCORE_ENVIRONMENT=Docker \
  -e ConnectionStrings__DefaultConnection="Server=mssql;Database=EmployeeManagement;User Id=sa;Password=Admin@123456;TrustServerCertificate=true;" \
  --network employee-network \
  --name employee-api \
  employee-api:latest
```

#### Build Frontend Image
```bash
cd employee-management-ui
docker build -t employee-ui:latest .
```

#### Run Frontend Container
```bash
docker run -d \
  -p 80:80 \
  --network employee-network \
  --name employee-ui \
  employee-ui:latest
```

### Docker Compose Services

The docker-compose.yml includes:

```yaml
services:
  mssql:           # SQL Server 2022 database
  backend:         # .NET 10 API
  frontend:        # Angular 19 UI (with nginx)
```

**Default Credentials:**
- **Database SA Account:**
  - User: `sa`
  - Password: `Admin@123456`

- **Application Accounts:**
  - Admin: `admin` / `admin123`
  - User: `user` / `user123`

### Docker Environment Variables

Configure in `docker-compose.yml`:

```yaml
environment:
  ASPNETCORE_ENVIRONMENT: "Docker"
  ConnectionStrings__DefaultConnection: "..."
  Jwt__Issuer: "EmployeeManagementAPI"
  Jwt__Audience: "EmployeeManagementUI"
  Jwt__Key: "YourSecretKey32CharactersMinimum"
```

### Docker Troubleshooting

**Issue:** "Port already in use"
- **Solution:** Stop other services or modify port in docker-compose.yml

**Issue:** "Cannot connect to database"
- **Solution:** Ensure mssql service is healthy: `docker-compose ps` and check health status

**Issue:** "Frontend can't reach backend"
- **Solution:** Services communicate via docker network; use `http://backend:5000` not `localhost:5000`

**View Logs:**
```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f backend
```

---

## API Documentation

### Swagger/OpenAPI

**Access:** http://localhost:5000/swagger

The API documentation includes:
- All available endpoints
- Request/response schemas
- HTTP methods and status codes
- Try-it-out functionality (interactive testing)

### API Endpoints

#### Authentication
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}

Response (200 OK):
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "role": "Admin"
}
```

#### Employee Endpoints

**List All Employees**
```http
GET /api/employees
Authorization: Bearer {token}

Response (200 OK):
[
  {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "department": "IT",
    "salary": 75000,
    "hireDate": "2023-01-15T00:00:00",
    "isActive": true
  }
]
```

**Get Employee by ID**
```http
GET /api/employees/{id}
Authorization: Bearer {token}

Response (200 OK):
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  ...
}
```

**Create Employee** (Admin Only)
```http
POST /api/employees
Authorization: Bearer {token}
Content-Type: application/json

{
  "firstName": "Jane",
  "lastName": "Smith",
  "email": "jane.smith@example.com",
  "phoneNumber": "098-765-4321",
  "department": "HR",
  "salary": 65000,
  "hireDate": "2023-02-01T00:00:00"
}

Response (201 Created):
{
  "id": 2,
  "firstName": "Jane",
  "lastName": "Smith",
  ...
}
```

**Update Employee**
```http
PUT /api/employees/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "firstName": "Jane",
  "lastName": "Smith",
  "email": "jane.smith@example.com",
  "department": "HR",
  "salary": 70000
}

Response (200 OK):
{
  "id": 2,
  "firstName": "Jane",
  ...
  "salary": 70000
}
```

**Delete Employee**
```http
DELETE /api/employees/{id}
Authorization: Bearer {token}

Response (204 No Content)
```

### API Response Formats

#### Success Response
```json
{
  "data": { /* response data */ },
  "success": true,
  "message": "Operation successful"
}
```

#### Error Response
```json
{
  "errors": ["Error message 1", "Error message 2"],
  "success": false,
  "message": "Validation failed"
}
```

### Status Codes

| Code | Meaning |
|------|---------|
| 200 | OK - Request successful |
| 201 | Created - Resource created successfully |
| 204 | No Content - Resource deleted successfully |
| 400 | Bad Request - Invalid input |
| 401 | Unauthorized - Missing/invalid authentication |
| 403 | Forbidden - Insufficient permissions |
| 404 | Not Found - Resource doesn't exist |
| 500 | Server Error - Internal server error |

### Postman Collection

Test API endpoints with Postman:

1. Download [Postman](https://www.postman.com/downloads/)
2. Import collection from `EmployeeManagement.API/EmployeeManagement.API.http`
3. Get JWT token from `/api/auth/login`
4. Use token for subsequent requests

---

## Authentication

### JWT (JSON Web Tokens)

The application uses JWT for stateless authentication:

**Flow:**
1. User submits credentials to `/api/auth/login`
2. Backend validates credentials
3. Backend generates JWT token containing user claims
4. Frontend stores token in `localStorage`
5. Frontend includes token in `Authorization` header for subsequent requests
6. Backend validates token on each request

**Token Example:**
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiJhZG1pbiIsImV0aWwiOm51bGwsImlzcyI6IkVtcGxveWVlTWFuYWdlbWVudEFQSSIsImF1ZCI6IkVtcGxveWVlTWFuYWdlbWVudFVJIiwiZXhwIjoxNzEwMjU1NDEzLCJpYXQiOjE3MTAyNTE4MTN9.
lc7Cz9...
```

### Default Users

**Admin User**
- Username: `admin`
- Password: `admin123`
- Role: Admin (full access)

**Regular User**
- Username: `user`
- Password: `user123`
- Role: User (limited access)

### Role-Based Access Control

| Feature | Admin | User |
|---------|-------|------|
| View Employees | ✓ | ✓ |
| Create Employee | ✓ | ✗ |
| Edit Employee | ✓ | ✗ |
| Delete Employee | ✓ | ✗ |
| View Dashboard | ✓ | ✓ |
| Manage Users | ✓ | ✗ |

### Changing Passwords

Currently, passwords are hardcoded. For production:

1. Implement password reset feature
2. Use secure password hashing (bcrypt)
3. Store salted hashes in database
4. Implement password strength validation
5. Add email verification

See [Future Improvements](#future-improvements) for planned security enhancements.

---

## Development Workflow

### Local Development Setup

1. **Clone Repository:**
   ```bash
   git clone https://github.com/shweta1732/Employee_Management_System.git
   cd EmployeeManagement
   ```

2. **Backend Setup:**
   ```bash
   cd EmployeeManagement.API
   dotnet restore
   dotnet build
   ```

3. **Frontend Setup:**
   ```bash
   cd ../employee-management-ui
   npm install
   ```

4. **Database Setup:**
   - Create SQL Server database (see Database Setup section)
   - Run migrations: `dotnet ef database update`

5. **Start Services:**
   - Terminal 1 (Backend): `cd EmployeeManagement.API && dotnet run`
   - Terminal 2 (Frontend): `cd employee-management-ui && npm start`

6. **Access Application:**
   - Frontend: http://localhost:4200
   - Backend: http://localhost:5000
   - Swagger: http://localhost:5000/swagger

### Git Workflow

```bash
# Create feature branch
git checkout -b feature/your-feature-name

# Make changes and commit
git add .
git commit -m "feat: describe your changes"

# Push to remote
git push origin feature/your-feature-name

# Create Pull Request on GitHub
# After review and approval, merge to develop then main
```

### Code Style & Conventions

**C# Backend:**
- Use PascalCase for classes, methods, properties
- Use camelCase for local variables
- Follow Microsoft C# naming guidelines
- Add XML documentation for public APIs

**TypeScript Frontend:**
- Use PascalCase for classes, interfaces
- Use camelCase for methods, properties
- Use kebab-case for component selectors
- Follow Google TypeScript style guide

### Commit Messages

Follow conventional commits format:
```
feat: add new feature
fix: fix bug
docs: update documentation
style: format code
refactor: restructure code
test: add tests
chore: update dependencies
```

---

## Testing

### Backend Testing

Unit tests are located in `EmployeeManagement.Tests/`

**Run All Tests:**
```bash
dotnet test
```

**Run Specific Test:**
```bash
dotnet test --filter "EmployeeServiceTests"
```

**Generate Coverage Report:**
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

**Test Projects:**
- `EmployeeControllerTests.cs` - API controller tests
- `EmployeeServiceTests.cs` - Business logic tests

### Frontend Testing

Tests are located in `src/app/**/*.spec.ts`

**Run Tests:**
```bash
npm test
```

**Run Tests with Coverage:**
```bash
npm test -- --code-coverage
```

**Run in Headless Mode (CI):**
```bash
npm test -- --watch=false --browsers=ChromeHeadless
```

### Integration Testing

Test end-to-end workflows:

```bash
# Backend API integration
npm run test:api

# Full application flow
npm run test:e2e
```

---

## Deployment

### Deployment to Production

#### Option 1: Docker & docker-compose (Recommended for Small/Medium)

1. **Prepare Production Environment:**
   ```bash
   # On production server
   cd /opt/employee-management
   git clone https://github.com/shweta1732/Employee_Management_System.git
   cd Employee_Management_System
   ```

2. **Update Configuration:**
   - Update `docker-compose.yml` with production database credentials
   - Set `ASPNETCORE_ENVIRONMENT=Production`
   - Update JWT secret key

3. **Deploy:**
   ```bash
   docker-compose pull
   docker-compose down
   docker-compose up -d
   ```

4. **Verify Deployment:**
   ```bash
   docker-compose ps
   # Check all services have healthy/running status
   ```

#### Option 2: Kubernetes (For Large Scale)

1. **Create deployment manifests:**
   ```yaml
   # backend-deployment.yaml
   apiVersion: apps/v1
   kind: Deployment
   metadata:
     name: employee-api
   spec:
     replicas: 3
     selector:
       matchLabels:
         app: employee-api
     template:
       metadata:
         labels:
           app: employee-api
       spec:
         containers:
         - name: api
           image: ghcr.io/shweta1732/employee-api:latest
           ports:
           - containerPort: 5000
           env:
           - name: ASPNETCORE_ENVIRONMENT
             value: "Production"
   ```

2. **Deploy to K8s:**
   ```bash
   kubectl apply -f backend-deployment.yaml
   kubectl apply -f frontend-deployment.yaml
   kubectl apply -f service.yaml
   kubectl apply -f ingress.yaml
   ```

#### Option 3: Cloud Platform (Azure App Service)

1. **Configure Azure resources:**
   - Create App Service for backend API
   - Create Static Web App for frontend
   - Create Azure SQL Database

2. **Deploy backend:**
   ```bash
   dotnet publish -c Release
   az webapp deployment source config-zip \
     --resource-group myResourceGroup \
     --name myApiAppService \
     --src publish.zip
   ```

3. **Deploy frontend:**
   ```bash
   npm run build
   az staticwebapp upload-files \
     --name myStaticWebApp \
     --source-path dist/employee-management-ui/browser
   ```

### Post-Deployment Verification

```bash
# Check API health
curl http://your-domain/api/health

# Test authentication
curl -X POST http://your-domain/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"admin123"}'

# Check frontend
curl http://your-domain/

# Monitor logs
docker-compose logs -f backend
```

### Monitoring & Maintenance

```bash
# View container logs
docker-compose logs -f

# Check resource usage
docker stats

# Backup database
docker-compose exec mssql /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P Admin@123456 \
  -Q "BACKUP DATABASE EmployeeManagement TO DISK='/var/opt/mssql/backup/EmployeeManagement.bak'"

# Restore database
docker-compose exec mssql /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P Admin@123456 \
  -Q "RESTORE DATABASE EmployeeManagement FROM DISK='/var/opt/mssql/backup/EmployeeManagement.bak'"
```

---

## Troubleshooting

### Common Issues

#### Backend Issues

**Issue: "The CORS protocol does not allow specifying a wildcard"**
```
Solution: Update CORS configuration in Program.cs with specific origins
```

**Issue: "Connection timeout to database"**
```
Solution: 
1. Check SQL Server is running: services.msc (Windows) or systemctl status (Linux)
2. Verify connection string in appsettings.json
3. Check firewall allows port 1433
```

**Issue: "Entity Framework migration fails"**
```
Solution:
dotnet ef migrations add InitialCreate
dotnet ef database update --context ApplicationDbContext
```

**Issue: "Port 5000 already in use"**
```
Solution:
# Find process using port 5000
netstat -ano | findstr :5000
# Kill process
taskkill /PID <PID> /F
```

#### Frontend Issues

**Issue: "Module not found '@angular/material'"**
```
Solution:
npm install @angular/material
npm install @angular/cdk
```

**Issue: "CORS error when calling API"**
```
Solution:
1. Ensure backend is running on http://localhost:5000
2. Check CORS policy in backend Program.cs allows frontend origin
3. Browser console will show specific error details
```

**Issue: "Cannot GET /employees after page refresh"**
```
Solution:
1. This is SPA routing issue
2. Configure web server to serve index.html for all routes
3. Already configured in nginx.conf for Docker
4. For local dev, Angular CLI handles this automatically
```

#### Docker Issues

**Issue: "Cannot connect container to container network"**
```
Solution:
1. Use service name (not localhost) for container-to-container communication
2. e.g., http://backend:5000 not http://localhost:5000
3. Verify docker network: docker network ls
```

**Issue: "Database keeps resetting after restart"**
```
Solution:
1. Add volume mount to preserve database:
   volumes:
     - sqldata:/var/opt/mssql
2. Data persists in sqldata volume
```

---

## Future Improvements

### Phase 8: Advanced Features

#### Performance & Scalability
- [ ] Implement Redis caching for frequently accessed data
- [ ] Add database query optimization and indexing
- [ ] Implement pagination for large datasets
- [ ] Add API rate limiting to prevent abuse
- [ ] Implement database connection pooling

#### Search & Filtering
- [ ] Advanced employee search with multiple criteria
- [ ] Full-text search capabilities
- [ ] Filter by department, salary range, hire date
- [ ] Export employee data to CSV/Excel
- [ ] Print-friendly employee directory

#### Notifications & Communication
- [ ] Email notifications for HR events
- [ ] In-app notifications using SignalR
- [ ] Slack integration for important alerts
- [ ] SMS notifications for urgent matters
- [ ] Calendar integration for events

#### Security Enhancements
- [ ] Two-factor authentication (2FA) support
- [ ] OAuth2 integration (Google, Microsoft, etc.)
- [ ] Implement rate limiting on authentication
- [ ] User session management and logout across devices
- [ ] Audit logging for sensitive operations
- [ ] Data encryption at rest and in transit

#### Reporting & Analytics
- [ ] Employee statistics dashboard
- [ ] Salary analysis and budget reports
- [ ] Departmental performance metrics
- [ ] Custom report builder
- [ ] Data visualization with charts and graphs
- [ ] Attendance tracking and reporting

#### Mobile Application
- [ ] React Native mobile app
- [ ] iOS/Android native apps
- [ ] Responsive mobile web interface
- [ ] Offline sync capabilities
- [ ] Push notifications

#### Admin Features
- [ ] User management interface
- [ ] Role and permission management
- [ ] System configuration interface
- [ ] Database backup and restore UI
- [ ] Activity logs viewer
- [ ] Email template management

#### Business Logic
- [ ] Employee leave management
- [ ] Performance review system
- [ ] Training and development tracking
- [ ] Org chart visualization
- [ ] Shift management
- [ ] Time tracking/attendance

#### Integration & APIs
- [ ] REST API versioning
- [ ] GraphQL API alternative
- [ ] Webhook support for custom integrations
- [ ] Calendar system integration (Google, Outlook)
- [ ] Document management system
- [ ] Third-party SAML/Active Directory integration

#### Testing & Quality
- [ ] End-to-end testing with Cypress/Playwright
- [ ] Performance testing and profiling
- [ ] Load testing for scalability verification
- [ ] Accessibility testing (WCAG 2.1)
- [ ] Security penetration testing

#### DevOps & Deployment
- [ ] Kubernetes deployment manifests
- [ ] Helm charts for automated deployment
- [ ] Infrastructure as Code (Terraform)
- [ ] CD/CD pipeline improvements
- [ ] Blue-green deployment strategy
- [ ] Automated database migrations in pipeline
- [ ] Container registry (Docker Hub / GitHub Packages)

#### Documentation
- [ ] API documentation improvements
- [ ] Architecture decision records (ADRs)
- [ ] Developer onboarding guide
- [ ] Video tutorials for common tasks
- [ ] Troubleshooting guide expansion
- [ ] Performance tuning guide

---

## Contributing

We welcome contributions! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/your-feature`
3. **Make** your changes and commit: `git commit -m "feat: describe changes"`
4. **Push** to your fork: `git push origin feature/your-feature`
5. **Create** a Pull Request to the main repository
6. **Wait** for code review and approval

### Code Review Checklist
- [ ] Code follows project style guidelines
- [ ] Comments added for complex logic
- [ ] Tests written and passing
- [ ] No console errors or warnings
- [ ] Documentation updated if needed

---

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## Support

For issues, questions, or suggestions:

- 📧 **Email:** support@employeemanagement.dev
- 💬 **Discussion Forum:** [GitHub Discussions](https://github.com/shweta1732/Employee_Management_System/discussions)
- 🐛 **Bug Reports:** [GitHub Issues](https://github.com/shweta1732/Employee_Management_System/issues)
- 📚 **Documentation:** [ARCHITECTURE.md](ARCHITECTURE.md) and [PLAN.md](PLAN.md)

---

## Project Status

**Current Version:** 1.0  
**Release Date:** March 2026  
**Status:** Stable - Ready for Production  

**Completed Phases:**
- ✅ Phase 1: Project Setup
- ✅ Phase 2: Backend Architecture
- ✅ Phase 3: Authentication
- ✅ Phase 4: Employee CRUD APIs
- ✅ Phase 5: Angular Frontend
- ✅ Phase 6: Docker Deployment
- ✅ Phase 7: Documentation

**Next Steps:**
- Advanced features from Future Improvements
- Kubernetes deployment
- Enterprise security enhancements

---

## Acknowledgments

- Built with [.NET 10](https://dotnet.microsoft.com/)
- Frontend powered by [Angular 19](https://angular.io/)
- Database: [SQL Server 2022](https://www.microsoft.com/sql-server/)
- Containerized with [Docker](https://www.docker.com/)
- Icons from [Bootstrap Icons](https://icons.getbootstrap.com/)

---

**Last Updated:** March 12, 2026  
**Maintained by:** Development Team  
**Repository:** https://github.com/shweta1732/Employee_Management_System

---

## Quick Links

- 🏠 [Project Home](https://github.com/shweta1732/Employee_Management_System)
- 📖 [Architecture Documentation](ARCHITECTURE.md)
- 📋 [Implementation Plan](PLAN.md)
- 🐛 [Report Issues](https://github.com/shweta1732/Employee_Management_System/issues)
- 💡 [Suggest Features](https://github.com/yourusername/Employee_Management_System/discussions)
