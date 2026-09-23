# Build/Test Dashboard

A Build/Test Dashboard application for tracking software builds 
and test results.

The application is being developed as a practical environment for 
developing and demonstrating skills with modern .NET development, 
including:

* **ASP.NET Core** Web API
* **Entity Framework Core** and database migrations
* **SQLite** and **SQL Server**
* **GitHub** and **Azure DevOps** integration
* **Docker** and **Kubernetes**
* **CI/CD**
* **Automated testing and code coverage**

The application is designed to run locally and retrieve build and 
test information from supported source-control and DevOps platforms 
rather than requiring build and test information to be entered 
manually.

The project is also intended to demonstrate software development 
practices such as separation of concerns, dependency injection, 
provider abstractions, automated testing, database schema 
evolution, and maintainable application architecture.

## Current Status

The application is under active development. Current functionality 
includes:

* Repository configuration for GitHub and Azure DevOps
* Repository validation through provider-specific implementations
* Local repository and credential storage abstractions
* Entity Framework Core data access
* SQLite database support
* SQL Server database support
* Provider-specific EF Core migration sets
* Automated unit and API testing

Additional build, test, dashboard, Docker, Kubernetes, and CI/CD 
functionality is planned as development continues.
