# 📚 Online Bookstore API (ASP.NET Core)

Welcome to the **Online Bookstore** project — a full-featured, secure, and scalable RESTful API built with **ASP.NET Core**, using **Entity Framework Core** and **PostgreSQL** as the database. This project demonstrates best practices in architecture, security, and maintainability.

---

## 🔧 Tech Stack

- **Backend Framework**: ASP.NET Core (.NET 8)
- **Authentication**: JWT Token Authentication
- **Authorization**: ASP.NET Core Identity
- **ORM**: Entity Framework Core
- **Database**: PostgreSQL
- **Middleware**:
  - Custom Authentication Middleware
  - Logging Middleware for request/response
- **Security Measures**:
  - SQL Injection prevention (EF Core)
  - XSS protection (encoded output / sanitization)
  - Strong password hashing (PBKDF2)
  - Input validation via Data Annotations
- **Development Practices**:
  - Dependency Injection (DI)
  - Repository Pattern (optional)
  - Logging via built-in ILogger

---

## 🗄️ Database Details

- **Database Name**: `online_bookstore`
- **Database Type**: PostgreSQL
- **Host**: Configured in `appsettings.json`
- **Username**: `postgres`
- **Password**: `password12345`

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=online_bookstore;Username=postgres;Password=password12345"
}
