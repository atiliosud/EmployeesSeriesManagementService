## Employees Series Management Service

A **full-stack C# and Blazor application** to manage employee addresses and their associated series based on specific business rules. Built as a coding exercise to demonstrate clean architecture, layered design, and code-first Entity Framework skills.

---

### Project Goals

This project was developed to showcase:

- ASP.NET Core Web API development
- Blazor WebAssembly UI application
- Entity Framework Core (Code-First)
- SQLite database integration
- Layered architecture: API, Business, DI, Data Access
- Unit testing for business logic
- Modern authentication and security practices

---

### Architecture

The project is structured into multiple logical layers:

- **Esms.API** – ASP.NET Core RESTful API exposing endpoints
- **Esms.App** – Blazor WebAssembly client application
- **Esms.Business** – Business logic and domain models
- **Esms.SQLITE** – Entity Framework DbContext and migrations
- **Esms.DependencyInjection** – Dependency injection setup
- **Esms.Business.Test** – Unit tests for core business logic

---

### Features Implemented

- Get addresses of a specific employee
- Get personal address of all employees where the **work city is 'Brussels'**
- Get series of an employee for a **specific period**
- Save a new series for an employee
- Front-end dropdown to filter **work city**, displaying personal addresses accordingly
- **JWT-based Authentication**
- **Role-based Authorization**
- **HTTPS redirection**
- **Input validation** using FluentValidation
- **Exception logging** via middleware

---

### Technologies Used

- ASP.NET Core 7.0
- Blazor WebAssembly
- Entity Framework Core (SQLite)
- C# 10
- FluentValidation
- xUnit for Unit Testing
- Minimal APIs & Dependency Injection
- JWT Bearer Authentication

---

### Security

The API is secured using:

- **JWT Authentication** with login endpoint (`/api/auth/login`)
- **Role-based access** using `[Authorize(Roles = "...")]`
- **HTTPS redirection** (`http → https`)
- **Validation layer** using FluentValidation to ensure all input is sanitized
- **Centralized error logging** via custom middleware

---

### Testing

- `Esms.Business.Test` includes unit tests for `EmployeeSeriesService`
- Mocked repository patterns for business-layer isolation
- Validation logic tested independently via FluentValidation

---

### 🚀 How to Run

1. Clone the repo  
   ```bash
   git clone https://github.com/[your-username]/EmployeesSeriesManagementService.git
   ```

2. Open the solution `Esms.sln` in Visual Studio

3. Apply migrations  
   Run this in **Package Manager Console**:
   ```powershell
   Update-Database
   ```

4. Start the **API project** (`Esms.API`) and the **Blazor app** (`Esms.App`)

5. Test the API at:
   ```
   https://localhost:{your-https-port}/swagger
   ```

---

### 🗂️ Notes

- Uses **code-first EF migrations**
- Shared `esms.db` file for persistence
- Modular and scalable architecture for future growth
- Example login:  
  ```json
  POST /api/auth/login  
  {
    "username": "admin",
    "password": "123456"
  }
  ```

---

