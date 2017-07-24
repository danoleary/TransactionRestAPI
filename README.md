# TransactionRestApi

### Requirements
* .NET Core 1.1

### Run App
* cd TransactionApi
* dotnet restore
* dotnet run

### Run Unit Tests
* cd TransactionApi.UnitTests
* dotnet restore
* dotnet test

### Run Integration Tests
* cd TransactionApi.IntegrationTests
* dotnet restore
* dotnet test

### Additional Features (given more time)
* Use real database (rather than in memory) for app and integration test
* Validate currency code against list of approved currencies
* Create dto class and mapping, as using [Required] tag and nullable types in model is pretty confusing
* Cover more exhaustive list of cases in unit and integration tests

### Time Spent
* 4 hours
