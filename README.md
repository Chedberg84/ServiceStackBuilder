# ServiceStackBuilder

Sick of adding the same boiler plate code to your server stack solution every time a new route is needed?
Do you want to move beyond copy, paste & rename?
Then ServiceStackBuilder is for you!

ServiceStackBuilder is a C# console application that automatically adds service stack CRUD routes to your visual studio solution.

Inputs:
- Sln file location
- New Request Object Name

Output:
- A Message class for each CRUD
- A Response class for each CRUD
- Service class with CRUD endpoints for your object
- Manager class with CRUD methods that hook into the repository.
- Repository class with CRUD endpoints for you to implement.
- Sets up new dependencies in the AppHost
- Unit Tests
- Automated Acceptance Tests

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

Note: if you have a similar project structure but you name your projects differently, that's okay! These variables can be changed in the app.config.

Happy Coding.
