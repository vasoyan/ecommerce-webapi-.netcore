# Getting started with .Net API 

This application follows the principles of Clean Architecture, where dependencies flow inwards, and the inner layers are not aware of the outer layers. This architecture is designed to make the system more maintainable, scalable, and testable. The use of Dependency Injection further enhances the testability and maintainability of the system by providing a way to swap out dependencies, such as substituting real repositories with mock ones during testing.

## ECommerce.API
This is the presentation layer of the application. It contains the API controllers which handle HTTP requests and responses, and the DependencyInjection folder suggests that this layer is responsible for setting up dependency injection.

## ECommerce.Application
This layer contains application-specific logic and interfaces (IServices). It also contains DTOs (Data Transfer Objects), VMs (View Models), and service implementations. The Mappings folder contains object mapping configurations, using a library like AutoMapper.

## ECommerce.Domain
This is the domain layer of the application. It contains business entities and repository interfaces (IRepositories).

## ECommerce.Infrastructure
This layer contains the actual implementations of the repositories defined in the Domain layer. It also contains the data context for interacting with the database and EntityTypeConfigurations which could be for configuring entity relationships and properties.

- Dependencies: This folder is currently empty. It might be used in the future to contain classes or files related to external dependencies of the Infrastructure layer.
- Context:
  `ECommerceDbContext.cs` This is the main DbContext class for Entity Framework, which represents a session with the database, allowing querying and saving of data.
- DependencyInjection:
  `InfrastructureExtensions.cs` This file might contain extension methods for setting up the Infrastructure layer services in the dependency injection container.
- EntityTypeConfigurations: This folder contains various configuration files which are used to configure the entity classes’ database schema using the Fluent API.
- Repositories: This folder contains the concrete implementations of the repository interfaces defined in the ECommerce.Domain layer. The Base Repository file is a generic repository that contains methods for common database operations like add, update, delete, and get. The other files in this folder are repositories for specific entities, inheriting from the base repository and possibly adding entity-specific database operations.
