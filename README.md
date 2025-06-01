# 📚 Online Bookstore API (ASP.NET Core)

Welcome to the **Online Bookstore** project — a full-featured, secure, and scalable RESTful API built with **ASP.NET Core**, using **Entity Framework Core** and **PostgreSQL** as the database. This project demonstrates best practices in architecture, security, and maintainability.

---

## 🔧 Tech Stack

- **Backend Framework**: ASP.NET Core (.NET 8)
- **Authentication**: JWT Token Authentication
- **Authorization**: ASP.NET Core Identity with **Role-Based Access Control (RBAC)**
  - Roles used: `Admin`, `User`
- **ORM**: Entity Framework Core (Code-First)
- **Database**: PostgreSQL
- **Middleware**:
  - ✅ Custom **Authentication Middleware**
  - ✅ Custom **Logging Middleware** (Logs requests & responses)
- **Security Measures**:
  - ✅ SQL Injection prevention using EF Core (parameterized queries)
  - ✅ Cross-Site Scripting (XSS) protection using safe encoding & validation
  - ✅ Secure password hashing (PBKDF2 via ASP.NET Identity)
  - ✅ Input validation using **Data Annotations** and **Model Binding**
- **Development Practices**:
  - Dependency Injection (built-in in ASP.NET Core)
  - Clean separation of concerns
  - Repository Pattern (optional extension)
  - Built-in logging using `ILogger<T>`

---

## 🗄️ Database Details

| Property        | Value                |
|----------------|----------------------|
| **Database**    | `online_bookstore`   |
| **Type**        | PostgreSQL           |
| **Host**        | `localhost`          |
| **Username**    | `postgres`           |
| **Password**    | `password12345`      |



### 🔗 Connection String Example (`appsettings.json`)
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=online_bookstore;Username=postgres;Password=password12345"
}
