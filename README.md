# Getting started with .Net API 

This application follows the principles of Clean Architecture, where dependencies flow inwards, and the inner layers are not aware of the outer layers. This architecture is designed to make the system more maintainable, scalable, and testable. The use of Dependency Injection further enhances the testability and maintainability of the system by providing a way to swap out dependencies, such as substituting real repositories with mock ones during testing.

## `Technical concepts used in this application`
- Dependency Injection (DI)
- Model-View-ViewModel (MVVM)
- AutoMapper
- JSON Serialization
- JWT (JSON Web Token) Authentication:
- Entity Framework Core (EF Core)
- Swagger
- CORS (Cross-Origin Resource Sharing)
- Error Logging
- ILogger (provides logging capabilities)
- Model Binding and Validation
- Response Handling


## API Project
This is the presentation layer of the application. It contains the API controllers which handle HTTP requests and responses, and the DependencyInjection folder suggests that this layer is responsible for setting up dependency injection.

- Common: This folder contains a file named Constants.cs, which stores constant values used in API project.
- Controllers: This folder houses multiple C# controller files. These controllers handle incoming HTTP requests and responses respectively.
- DependencyInjection: This folder contains configurations for dependency injection.
- appsettings.json: This file typically contains configuration settings for the application.
- Program.cs: This is a C# file that contains the main entry point for the application.

## Application Project
This layer contains application-specific logic and interfaces (IServices). It also contains DTOs (Data Transfer Objects), VMs (View Models), and service implementations. The Mappings folder contains object mapping configurations, using a library like AutoMapper.

- Dependencies: `ApplicationExtensions.cs` file contains the dependencies for this Application layer of Application.IServices and Application.Services. 
- IServices: This folder contains interfaces that define the operations supported by the application. These interfaces are implemented in the Services folder. The `IBaseService.cs` file is a generic service interface that contains methods for common operations like `GetAllAsList`, `GetByIdAsync`, and `DeleteByIdAsync`.
- Mappings: This folder contains object-to-object mapping configurations, using a library like AutoMapper to convert between Entity objects and DTOs (Data Transfer Objects).
- Models: This folder contains DTOs and VMs (View Models) used for data transfer between layers. The DTOs subfolder contains Data Transfer Object files like `BaseDTO.cs`, `BrandDTO.cs`, etc., used for transferring data between layers. The VMs subfolder contains ViewModel files like `BaseViewModel.cs`.
- Helpers: This folder contains helper classes that provide utility functions used across the application. The ApiResponse.cs file might be a class that standardizes the format of responses sent from the API.
- Services: This folder contains the concrete implementations of the service interfaces defined in the IServices folder. The BaseService.cs file is a generic service that contains methods for common operations. The other files in this folder (like BrandService.cs, CategoryService.cs, etc.) are services for specific entities, inheriting from the base service and possibly adding entity-specific operations.

## Domain Project
This is the domain layer of the application. It contains business entities and repository interfaces (IRepositories).

- Entities: This folder contains the business entities of the application. These are classes that represent the various business objects that the application will be dealing with.
- Repositories: This folder contains interfaces for the repositories. These interfaces define the operations that can be performed on the entities. For example, the IBaseRepository interface methods like GetAllAsync, GetByIdAsync, SaveAsync, and UpdateAsync. These methods are used to perform common database operations. There are also other repository interfaces, which are specific to certain entities.

## Infrastructure Project
This layer contains the actual implementations of the repositories defined in the Domain layer. It also contains the data context for interacting with the database and EntityTypeConfigurations which could be for configuring entity relationships and properties.

- Dependencies: file contains the dependencies for this Infrastructure layer of Domain.IRepositories and Infrastructure.Repositories.
- Context: `ECommerceDbContext.cs` This is the main DbContext class for Entity Framework, which represents a session with the database, allowing querying and saving of data.
- DependencyInjection: `InfrastructureExtensions.cs` This file might contain extension methods for setting up the Infrastructure layer services in the dependency injection container.
- EntityTypeConfigurations: This folder contains various configuration files which are used to configure the entity classes’ database schema using the Fluent API.
- Repositories: This folder contains the concrete implementations of the repository interfaces defined in the ECommerce.Domain layer. The Base Repository file is a generic repository that contains methods for common database operations like add, update, delete, and get. The other files in this folder are repositories for specific entities, inheriting from the base repository and possibly adding entity-specific database operations.
