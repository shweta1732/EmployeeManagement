# Employee Management System - Implementation Plan

This document outlines a detailed, phased implementation plan for a production-ready Employee Management System using the specified technologies.  
**Note:** the backend will target **.NET 10** and the frontend will use **Angular 19** (or the latest stable release) to match the user's environment.  

The Angular project has been upgraded to version 19, with dependencies and configuration updated accordingly.

## Phase 1: Project Setup

**Goal:**
- Establish the core repository structure for both backend and frontend.
- Configure tooling, dependencies, and initial project scaffolding.
- Generate minimal runnable boilerplate for API and UI so developers can build and verify functionality locally before committing.

**Tasks:**
1. Initialize Git repository and branch strategy (main, develop, feature/*).
2. Create solution folder for .NET backend targeting **.NET 10**.
   - `EmployeeManagement.Api` (Web API project)
   - `EmployeeManagement.Core` (domain models, DTOs, interfaces)
   - `EmployeeManagement.Infrastructure` (EF Core, repository implementations)
   - `EmployeeManagement.Services` (business logic/services)
3. Set up Angular workspace with Angular CLI (version 19 or latest).
   - `employee-management-app` Angular project.
4. Add Angular Material, Bootstrap, RxJS, Reactive Forms dependencies.
5. Configure Docker support (Dockerfiles for API and Angular app).
6. Create GitHub Actions workflows skeleton for CI, ensuring .NET 10 and Angular 19 toolchains.
7. Update `README.md`, `ARCHITECTURE.md` with initial notes.

**Expected files:**
- `EmployeeManagement.sln`
- Projects directories (`Api`, `Core`, `Infrastructure`, `Services`)
- Angular project folder `employee-management-app/`
- `Dockerfile` and `docker-compose.yml` placeholders
- GitHub Actions workflow YAMLs under `.github/workflows/`
- Empty `PLAN.md` (this document), `README.md`, `ARCHITECTURE.md` updated

**Dependencies:**
- .NET 10 SDK installed
- Node.js/npm for Angular 19
- GitHub repository set up
- Docker installed

> ✅ **Phase 1 implemented:** backend solution structure created with initial projects, references, and runnable API boilerplate. Code compiles and `dotnet run` starts the server.

## Phase 2: Backend Architecture

**Goal:**
- Implement foundational backend components: domain models, DbContext, repository pattern, service layer, AutoMapper, validation.

**Tasks:**
1. Define domain entities (Employee, Department, etc.) in `Core`.
2. Configure `DbContext` in `Infrastructure` with EF Core.
3. Implement generic repository interface and base class.
4. Create specific repositories for entities.
5. Develop service interfaces and implementations using repositories.
6. Configure AutoMapper profiles.
7. Add FluentValidation validators for models/DTOs.
8. Register dependencies in `Api` startup/Program.cs.

**Expected files:**
- `Core/Entities/Employee.cs`, `Department.cs`, etc.
- `Core/DTOs/*`
- `Infrastructure/Data/ApplicationDbContext.cs`
- `Infrastructure/Repositories/IRepository.cs`, `Repository.cs`, `EmployeeRepository.cs`
- `Services/Interfaces/IEmployeeService.cs` etc.
- `Services/Implementations/EmployeeService.cs`
- `Api/Mapping/MappingProfile.cs`
- `Core/Validators/EmployeeValidator.cs`

**Dependencies:**
- EF Core packages
- AutoMapper
- FluentValidation

> ✅ **Phase 2 implemented:** domain entities, DbContext, repository pattern, service layer, dependency injection, and Swagger configured. API project successfully starts and responds to requests (weather forecast endpoint plus employee controller placeholder).

## Phase 3: Authentication

**Goal:**
- Secure the API using JWT and implement user management with roles.

**Tasks:**
1. Add `User` entity and simple user store.
2. Configure JWT settings in `appsettings.json` and create `JwtTokenService`.
3. Implement `AuthService` to validate credentials and issue tokens with claims.
4. Create `AuthController` with `POST /api/auth/login`.
5. Protect employee endpoints using `[Authorize]` and role attributes (`Admin`, `User`).
6. Seed sample users (`admin/admin123` and `user/user123`) in startup.

**Expected files:**
- `Core/Entities/User.cs`
- `Application/DTOs/LoginDto.cs`, `AuthResponseDto.cs`
- `Application/Interfaces/IAuthService.cs`, `ITokenService.cs`
- `Application/Services/AuthService.cs`
- `API/Services/JwtTokenService.cs`
- `API/Controllers/AuthController.cs`
- `appsettings.json` with JWT configuration

**Dependencies:**
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity for password hashing

> ✅ **Phase 3 implemented:** JWT authentication wired up, user/password hashing in place, role-based authorization applied to employee controller, and sample admin/user accounts seeded. API runs with in-memory database and login endpoint issues tokens.

## Phase 4: Employee CRUD APIs

**Goal:**
- Expose REST endpoints for creating, reading, updating, and deleting employees with full input validation and mapping.

**Tasks:**
1. Define DTOs for employee operations (create/read/update) and register mappings.
2. Implement `EmployeeController` with endpoints:
   - GET /employees
   - GET /employees/{id}
   - POST /employees
   - PUT /employees/{id}
   - DELETE /employees/{id}
   (controllers now accept/return DTOs and use AutoMapper)
3. Ensure validation with FluentValidation validators targeting the DTOs.
4. Use the service layer for business logic; service already returns entities.
5. Add unit test project (`EmployeeManagement.Tests`) and write tests for the service and controller using xUnit and Moq.

**Expected files:**
- `Api/Controllers/EmployeeController.cs` (updated with DTO usage)
- `Application/DTOs/EmployeeCreateDto.cs`, `EmployeeUpdateDto.cs`, `EmployeeReadDto.cs`
- `Application/Validators/EmployeeCreateDtoValidator.cs`, `EmployeeUpdateDtoValidator.cs`, `EmployeeReadDtoValidator.cs`
- `Api/Mapping/MappingProfile.cs` updated
- `Application/Services/EmployeeService.cs` already implemented
- `EmployeeManagement.Tests/` project with `EmployeeServiceTests.cs` and `EmployeeControllerTests.cs`

**Dependencies:**
- xUnit for testing
- Moq for mocking

> ✅ **Phase 4 implemented:** DTOs and validators added, AutoMapper configured, controller updated to handle DTOs, and a test project with basic unit tests created. All code compiles and API passes our smoke tests (Login + basic employee operations with in-memory store).

## Phase 5: Angular Frontend

**Goal:**
- Deliver a fully-functional Angular 15 frontend called `employee-management-ui` that connects to the backend APIs with authentication and CRUD flows.

**Tasks:**
1. Create Angular workspace (`employee-management-ui`) with routing and required dependencies (Material, Bootstrap) targeting **Angular 19**.
2. Establish folder structure under `src/app` (components, services, models, guards, interceptors, pages).
3. Implement pages for login, dashboard, employee list, add, edit.
4. Configure routing with `AuthGuard` to protect routes.
5. Develop services (`AuthService`, `EmployeeService`) interacting with API endpoints and handle JWT token storage.
6. Add JWT interceptor to append Authorization header.
7. Use Angular Material table with pagination, sorting and filter for employee list.
8. Build reactive forms with validation for login, add, and edit pages.
9. Dashboard shows total employees, active employees (placeholder logic), and department count.
10. Wire up components to call backend and handle navigation/actions (edit/delete).
11. Include environment configuration for API URL.

**Expected files:**
- `employee-management-ui/` folder containing Angular project files
- `src/app/app.module.ts`, `app-routing.module.ts`, and `app.component.ts`
- Models in `models/`
- Services in `services/` with HTTP calls
- Guards and interceptors in respective folders
- Pages under `pages/` with HTML/TS for each feature
- Bootstrap/Material styling on pages

**Dependencies:**
- Angular 15 packages
- `@angular/material` and `bootstrap`
- RxJS & Angular core libraries

> ✅ **Phase 5 implemented:** UI skeleton created with all requested pages, routing, guards, services, and components. Code is structured for Angular CLI and will build/serve after running `npm install` inside `employee-management-ui` (note: project scaffolded manually due to environment limitations). UI code calls backend endpoints and includes JWT interceptor.

## Phase 6: Docker Deployment

**Goal:**
- Containerize backend and frontend, enable local development and production deployment.

**Tasks:**
1. Write Dockerfile for .NET API.
2. Write Dockerfile for Angular app (multi-stage build).
3. Create `docker-compose.yml` to orchestrate API, Angular, and SQL Server.
4. Configure environment variables/secrets.
5. Test building and running containers locally.
6. Update GitHub Actions workflow to build Docker images and push to registry.

**Expected files:**
- `Dockerfile` (API)
- `Dockerfile` (Angular)
- `docker-compose.yml`
- Updated `.github/workflows/` with Docker build steps

**Dependencies:**
- Docker Desktop
- Container registry (GitHub Packages, Docker Hub)

## Phase 7: Documentation

**Goal:**
- Provide comprehensive docs for setup, deployment, architecture, and API usage.

**Tasks:**
1. Fill out `README.md` with project overview and quick start.
2. Expand `ARCHITECTURE.md` with diagrams and decisions.
3. Document API endpoints (Swagger/OpenAPI).  
4. Create developer guides for backend and frontend.
5. Add comments and inline docs in code.

**Expected files:**
- `README.md` updated
- `ARCHITECTURE.md` details
- Swagger UI enabled in API
- Additional markdown guides if necessary

**Dependencies:**
- Swashbuckle or similar for Swagger
- Diagram tools if needed


---

This plan will be saved to `PLAN.md`.  Once you confirm, development can begin accordingly.
