# 🏛️ Architecture

## 📚 Layers

📦 API Layer  
   └── Handles HTTP requests, Controllers & Routing (depends on Host)  

📦 Host Layer  
   └── Entry point, DI, Configuration (depends on Domain, Application, Infrastructure)  

📦 Application Layer  
   └── Business logic, Services, DTOs, Authorization, Mappings (depends only on Domain)  

📦 Infrastructure Layer  
   └── Persistence, EF Core, Repositories, UnitOfWork, Data Seeding (depends only on Domain)  

📦 Domain Layer  
   └── Core Entities, Enums, Interfaces (no dependencies)

---

## 🔗 Layer Diagram

                  +--------------------------+
                  |        API Layer         |
                  |--------------------------|
                  | Handles HTTP requests    |
                  | Controllers & Routing    |
                  | Depends only on Host     |
                  +------------+-------------+
                               |
                               v
                  +-----------------------------+
                  |        Host Layer           |
                  |-----------------------------|
                  | Entry point, DI, Config     |
                  | Depends on Domain,          |
                  | Application, Infrastructure |
                  +------+--------+-------------+
                         |            |
                         v            v
            +----------------+  +-------------------+
            | Application    |  | Infrastructure    |
            | Layer          |  | Layer             |
            |----------------|  |-------------------|
            | Business logic |  | Persistence, EF   |
            | DTOs, Services |  | Repositories      |
            | Depends on     |  | Depends on Domain |
            | Domain         |  |                   |
            +----------------+  +-------------------+
                   |                      |
                   v                      v
                  +--------------------------+
                  |       Domain Layer       |
                  |--------------------------|
                  | Core Entities, Enums,    |
                  | Interfaces               |
                  | No dependencies          |
                  +--------------------------+

## 🔗 Files Structure
## 🔗 Project Files Structure

### 📁 src

---

#### 📁 API
- **Controllers**
  - **Admin**
    - **Custom**
      - `LanguagesController.cs`
    - **ManageUsers**
      - `PermissionsController.cs`
      - `RolesController.cs`
  - **Authentication**
    - `LoginController.cs`
    - `RegistrationController.cs`
    - `ProfileController.cs`
  - **LibraryServices**
    - `BorrowingsController.cs`
    - `ReservationController.cs`
    - `SubscriptionController.cs`
- `appsettings.json`

---

#### 📁 Application
- **Authorization**
  - **Configuration**
    - `JWT.cs`
  - **Interfaces**
    - `IJwtTokenService.cs`
  - **Services**
    - `CurrentUser.cs`
    - `JwtTokenService.cs`
    - `PermissionHandler.cs`
    - `PermissionPolicyProvider.cs`
- **Dtos**
  - Request/
  - Response/
  - Security/
- Exceptions/
- Features/
  - Interfaces/
  - Services/
- Mappings/
  - `MappingProfile.cs`
- `DependencyInjection.cs`

---

#### 📁 Domain
- Constants/
- **Entities**
  - Common/
    - `BaseEntity.cs`
  - Security/
    - `Permission.cs`
    - `Role.cs`
    - `RolePermission.cs`
    - `User.cs`
- Enums/
- **Interfaces**
  - `IUnitOfWork.cs`
  - `IValidatableEntity.cs`
  - `IBaseRepository.cs`
  - Security/
    - `ICurrentUser.cs`

---

#### 📁 Host
- Extensions/
  - `ApiServiceExtensions.cs`
- Middlewares/
  - `CorrelationMiddleware.cs`
  - `ExceptionHandlingMiddleware.cs`
- Seeder/
  - `StartupSeeder.cs`
- SwaggerConfig/
  - `AuthorizeCheckOperationFilter.cs`
  - `SwaggerSettings.cs`
- `DependencyInjection.cs`
- `Program.cs`

---

#### 📁 Infrastructure
- Configurations/
- Context/
  - `LibraryDbContext.cs`
  - `LibraryDbContextFactory.cs`
- DataSeed/
  - Migration/
  - RunTime/
- Extensions/
- Repositories/
  - `BaseRepository.cs`
  - `UnitOfWork.cs`
- Migrations/
- `DependencyInjection.cs`

---

#### 📁 Test


