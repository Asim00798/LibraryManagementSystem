## Architecture
## Layers

- **API**  
  Handles HTTP requests, controllers, and routing. Depends **only on Host**.  

- **Host**  
  Entry point for the application. Configures services, dependency injection, and runs the application. Depends on **Domain, Application, and Infrastructure**.  

- **Application**  
  Contains business logic, services, DTOs, authorization, and mappings. Depends only on **Domain**.  

- **Domain**  
  Core entities, enums, and interfaces. Has **no dependencies**.  

- **Infrastructure**  
  Handles persistence, EF Core, repositories, Unit of Work, and data seeding. Depends only on **Domain**.  

## View
graph TD
    %% Clusters
    subgraph Layer_API [📦 API Layer]
        direction TB
        API["- Handles HTTP requests\n- Controllers & Routing\n- Depends only on Host"]
    end

    subgraph Layer_Host [📦 Host Layer]
        direction TB
        Host["- Entry point, DI, Configuration\n- Depends on Domain, Application, Infrastructure"]
    end

    subgraph Layer_AppInfra [Application & Infrastructure]
        direction LR
        subgraph Layer_App [📦 Application Layer]
            Application["- Business logic, Services, DTOs, Authorization, Mappings\n- Depends only on Domain"]
        end
        subgraph Layer_Infra [📦 Infrastructure Layer]
            Infrastructure["- Persistence, EF Core, Repositories, UnitOfWork, Data Seeding\n- Depends only on Domain"]
        end
    end

    subgraph Layer_Domain [📦 Domain Layer]
        direction TB
        Domain["- Core Entities, Enums, Interfaces\n- No dependencies"]
    end

    %% Node styles
    style API fill:#f9f,stroke:#333,stroke-width:2px,stroke-dasharray: 5 5
    style Host fill:#bbf,stroke:#333,stroke-width:2px
    style Application fill:#bfb,stroke:#333,stroke-width:2px
    style Infrastructure fill:#ffb,stroke:#333,stroke-width:2px
    style Domain fill:#fff,stroke:#333,stroke-width:2px

    %% Dependencies with curved arrows
    API -->|calls| Host
    Host -->|depends on| Application
    Host -->|depends on| Infrastructure
    Host -->|depends on| Domain
    Application -->|depends on| Domain
    Infrastructure -->|depends on| Domain

## Files Structure
📁 src/
├─ 📁 API/
│  ├─ 📁 Controllers/
│  │  ├─ 📁 Admin/
│  │  │  ├─ 📁 Custom/
│  │  │  │  └─ 📄 LanguagesController.cs
│  │  │  └─ 📁 ManageUsers/
│  │  │     ├─ 📄 PermissionsController.cs
│  │  │     └─ 📄 RolesController.cs
│  │  │
│  │  ├─ 📁 Authentication/
│  │  │  ├─ 📄 LoginController.cs
│  │  │  ├─ 📄 RegistrationController.cs
│  │  │  └─ 📄 ProfileController.cs
│  │  │
│  │  └─ 📁 LibraryServices/
│  │     ├─ 📄 BorrowingsController.cs
│  │     ├─ 📄 ReservationController.cs
│  │     └─ 📄 SubscriptionController.cs
│  │
│  └─ 📄 appsettings.json
│
├─ 📁 Application/
│  ├─ 📁 Authorization/
│  │  ├─ 📁 Configuration/
│  │  │  └─ 📄 JWT.cs
│  │  ├─ 📁 Interfaces/
│  │  │  └─ 📄 IJwtTokenService.cs
│  │  └─ 📁 Services/
│  │     ├─ 📄 CurrentUser.cs
│  │     ├─ 📄 JwtTokenService.cs
│  │     ├─ 📄 PermissionHandler.cs
│  │     └─ 📄 PermissionPolicyProvider.cs
│  │
│  ├─ 📁 Dtos/
│  │  ├─ 📁 Request/
│  │  ├─ 📁 Response/
│  │  └─ 📁 Security/
│  │
│  ├─ 📁 Exceptions/
│  │
│  ├─ 📁 Features/
│  │  ├─ 📁 Interfaces/
│  │  └─ 📁 Services/
│  │
│  ├─ 📁 Mappings/
│  │  └─ 📄 MappingProfile.cs
│  └─ 📄 DependencyInjection.cs
│
├─ 📁 Domain/
│  ├─ 📁 Constants/
│  ├─ 📁 Entities/
│  │  ├─ 📁 Common/
│  │  │  └─ 📄 BaseEntity.cs
│  │  └─ 📁 Security/
│  │     ├─ 📄 Permission.cs
│  │     ├─ 📄 Role.cs
│  │     ├─ 📄 RolePermission.cs
│  │     └─ 📄 User.cs
│  ├─ 📁 Enums/
│  └─ 📁 Interfaces/
│     ├─ 📄 IUnitOfWork.cs
│     ├─ 📄 IValidatableEntity.cs
│     ├─ 📄 IBaseRepository.cs
│     └─ 📁 Security/
│        └─ 📄 ICurrentUser.cs
│
├─ 📁 Host/
│  ├─ 📁 Extensions/
│  │  └─ 📄 ApiServiceExtensions.cs
│  ├─ 📁 Middlewares/
│  │  ├─ 📄 CorrelationMiddleware.cs
│  │  └─ 📄 ExceptionHandlingMiddleware.cs
│  ├─ 📁 Seeder/
│  │  └─ 📄 StartupSeeder.cs
│  ├─ 📁 SwaggerConfig/
│  │  ├─ 📄 AuthorizeCheckOperationFilter.cs
│  │  └─ 📄 SwaggerSettings.cs
│  ├─ 📄 DependencyInjection.cs
│  └─ 📄 Program.cs
│
├─ 📁 Infrastructure/
│  ├─ 📁 Configurations/
│  ├─ 📁 Context/
│  │  ├─ 📄 LibraryDbContext.cs
│  │  └─ 📄 LibraryDbContextFactory.cs
│  ├─ 📁 DataSeed/
│  │  ├─ 📁 Migration/
│  │  └─ 📁 RunTime/
│  ├─ 📁 Extensions/
│  ├─ 📁 Repositories/
│  │  ├─ 📄 BaseRepository.cs
│  │  └─ 📄 UnitOfWork.cs
│  ├─ 📁 Migrations/
│  └─ 📄 DependencyInjection.cs
│
└─ 📁 Test/
