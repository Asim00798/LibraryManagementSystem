# Instructions – Library Management System

## 📖 Overview
The **Library Management System** is built with **ASP.NET Core Web API (.NET 8 LTS)** using **Entity Framework Core** and **Repository Pattern**.  
It manages books, users, staff, memberships, borrowings, and payments across multiple library branches.

---

## ⚙️ Requirements
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Express)  
- Git  

---

## 📂 Project Structure
See [docs/Architecture.md](docs/Architecture.md) for a full diagram.

---

## 🛠️ Setup

## Quick Start

1. Clone the repository:
   git clone https://github.com/Asim00798/LibraryManagementSystem
2. Apply EF Initial migration.
3. Set connection string on appsettings.json.
4. Run the API:

The API will be available at:
👉 https://localhost:5001/api/v1

Example login seeded user in runtime :
Username: Admin
Password: Admin@12345

Note:
you can find other seeded users and roles and permissions in infrastructure data seed file.

