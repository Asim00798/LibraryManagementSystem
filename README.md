# 🚀 Library Management System API

Library Management backend API built with **.NET 8 Web API** and **Clean Architecture**, designed for scalability, maintainability, and high performance.

---

## 🎯 Project Goal

Build a smart platform for managing libraries, books, authors, borrowings, reservations, and memberships, with features like offline payments, subscriptions, multi-language support, and fine-grained role & permission management.

---

## 🏗 Architecture & Layers

- **API Layer** – Handles HTTP requests, controllers, and routing  
- **Host Layer** – Entry point, dependency injection, and service configuration  
- **Application Layer** – Business logic, DTOs, services, authorization, and mappings  
- **Domain Layer** – Entities, enums, and interfaces (core business rules)  
- **Infrastructure Layer** – Database context, EF Core configurations, repositories, UnitOfWork, and data seeding  

This layered design ensures separation of concerns, testability, and maintainability.

---

## 🛠 Tech Stack

- **.NET 8 Web API** – Clean Architecture  
- **Entity Framework Core + SQL Server** – Data persistence  
- **JWT Authentication** – Role & permission-based authorization  
- **AutoMapper** – DTO ↔ Entity mapping  
- **FluentValidation & Data Annotations** – Strong input validation  
---

## 📦 Database & Key Entities

- **Users** – Admin, Staff, Members  
- **Books & Authors** – Management, reviews, and relationships  
- **Borrowings & Reservations** – Track book loans and reservations  
- **Subscriptions & Memberships** – Offline payment handling and plan management  
- **Payments & Audit Logs** – Offline payment records and real-time tracking  
- **Languages & Events** – Multi-language support and events management  

---

## ⚡ Key Features (Implemented)

- **Secure User Authentication** – JWT login  
- **Role & Permission Management** – Dynamic access control  
- **CRUD Operations** – Books, Authors, Borrowings, Reservations, Subscriptions  
- **Offline Payments & Memberships** – Track and manage payments  
- **Audit Logging & Soft Delete** – Real-time tracking of performed actions  
- **Multi-language Management** – Add and manage supported languages  

---

## 🔮 Future Enhancements

- **Staff Management** – Full staff CRUD and attendance tracking  
- **Events Management** – Library events and scheduling  
- **Notifications System** – Real-time alerts for members and staff  
- **OTP & SMS Authentication** – Enhanced login security  
- **Online Payment Integration** – Secure online payment gateways  
- **Mini Social Platform** – Author profiles, reviews, and interactions  

---

## 💡 Why This Project Matters

- Implements modern backend best practices  
- Scalable, maintainable, and testable architecture  
- Secure authentication and authorization  
- Integrates real-world library operations and offline payment management  

---

## 🏷 Tags

`.NET 8` `.AspNetCore` `.WebAPI` `.CSharp` `.CleanArchitecture` `.JWT` `.EFCore` `.FluentValidation` `.MailKit` `.Serilog` `.Localization` `.SoftwareEngineering` `.LibraryManagement` `.BackendDevelopment` `.MultiLayeredArchitecture` `.DatabaseDesign`
