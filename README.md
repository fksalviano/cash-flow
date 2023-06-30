# CashFlow

.Net Core API using Clean Architecture and Vertical Slice

Architecture implemented to isolate the Application Common Domain from each Use Case Domain.

### Use Cases

- **SaveTransaction**:
    Register a new Credit or Debit Transaction.

- **ListTransactions**:
    List all registered Transactions.

- **GetDailyBalance**:
    Get consolidated balance by day.

### API

The API micro-service with the Controllers and endpoints.

### Dependency Injection

The project uses Dependence Injection to build the container services and uses self-installers to add each Use Case on the container independently.

## Tests

- **Unit Tests**:
    Tests for each class and methods independently.

- **Integrated Tests**:
    Tests for each endpoint runnig the application (**necessary to run docker-compose before run the tests**).

## Configuration

### Requirements

Need to install the follow:

- Git:
    https://git-scm.com/downloads

- Dotnet Core 7.0 SDK and Runtime:
    https://dotnet.microsoft.com/en-us/download/dotnet/7.0


## Getting Started

#### Clone the repository:

```bash
git clone https://github.com/fksalviano/cash-flow.git
```

#### Go to the project directory

```bash
cd cash-flow
```

#### Build the project

```bash
dotnet build
```

#### Up Docker Database Container (Starts the Database and Run SQL Scripts)

```bash
docker-compose up -d
```

#### Run tests

```bash
dotnet test
```

#### Run the project

```bash
dotnet run --project src/API
```

#### Open Swagger API documentation
- http://localhost:5210/swagger


![API Swagger Doc](swagger.png?raw=true "API Swagger Doc")

## Packages

The project uses the following packages

- Dapper:
    https://www.nuget.org/packages/Dapper/
    
- FluentValidation:
    https://www.nuget.org/packages/fluentvalidation

- FluentAssertions:
    https://www.nuget.org/packages/fluentassertions

- XUnit:
    https://www.nuget.org/packages/xunit

- AutoFixture:
    https://www.nuget.org/packages/autofixture

- Moq.AutoMock:
    https://www.nuget.org/packages/moq.automock
