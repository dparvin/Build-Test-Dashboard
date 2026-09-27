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
* **Event-driven data synchronization and webhooks**
* **SignalR** for real-time dashboard updates
* **Docker** and **Kubernetes**
* **CI/CD**
* **Automated testing and code coverage**

The application is designed to run locally and retrieve build and 
test information from supported source-control and DevOps platforms 
rather than requiring build and test information to be entered 
manually.

Build information is synchronized into a local database so that the 
dashboard can continue to provide historical information even when 
the external services or the application itself are temporarily 
unavailable. Event-driven notifications are intended to provide 
near-real-time updates when new builds are detected, while 
synchronization on application startup will reconcile data that may 
have been created while the application was not running.

The project is also intended to demonstrate software development 
practices such as separation of concerns, dependency injection, 
provider abstractions, event-driven integration, automated testing, 
database schema evolution, and maintainable application architecture.

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
* Repository and build data models and persistence
* Automated unit and API testing

The following functionality is planned or under development:

* Build and test data synchronization
* Webhook-based build notifications
* Test result retrieval and persistence
* Web dashboard
* SignalR real-time dashboard updates
* Startup synchronization and reconciliation
* Docker containerization
* Kubernetes deployment
* CI/CD automation
