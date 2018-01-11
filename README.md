# ServiceStackBuilder

A C# console application that automatically adds service stack CRUD routes for you.

Inputs:
- Sln file location
- New Request Object Name

Output:
- A Message class
- A Response class
- Creates a new service file with CRUD endpoints for your object
- Creates a new Manager class
- Creates a new Repository class
- Sets up new dependencies in the AppHost (Container Manager)

Requirements:
Requires a specific service stack sln structure to work properly

- <slnName>.csproj: Web Project with AppHost named the same as the sln file
- <slnName>.Interfaces.csproj: library to hold interface definitions
- <slnName>.Managers.csproj: library to hold manager definitions
- <slnName>.Repositories.csproj: library to hold repository definitions
- <slnName>.ServiceDefinition.csproj: a library to hold route definitions
- <slnName>.ServiceModel.csproj: a library to hold request / response messages
- <slnName>.UnitTests.csproj: a library for unit testing
- <slnName>.AcceptanceTests.csproj: a library for automated acceptance testing

