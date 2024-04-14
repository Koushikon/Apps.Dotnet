# Unit Test with Dapper

- [Learn Source](https://enlear.academy/how-to-write-unit-tests-with-dapper-d97f4c6f76d6)

Writing unit tests when using the Dapper ORM tool for database operations in an ASP.NET Core Web Application can be complex. This is because Dapper uses static extension methods that are difficult to mock when testing services.

There is one approach to resolving this problem. We need to create a generic database service which uses Dapper implementation with the interface of that service. We can inject the database service using dependency injection. Then we can mock that database service when using it for other services.