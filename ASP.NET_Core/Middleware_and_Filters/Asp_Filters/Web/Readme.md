# Filters in ASP.Net MVC

[MS Docs][lnk0001] | [Docs 1][lnk0002] | [Docs 2][lnk0003]

This project implements different type of filters:

- `Authorization filters` – They run first to determine whether a user is authorized for the current request
- `Resource filters` – They run right after the authorization filters and are very useful for caching and performance
- `Action filters` – They run right before and after the action method execution
- `Exception filters` – They are used to handle exceptions before the response body is populated
- `Result filters` – They run before and after the execution of the action methods result.


## Default order of execution:

- The before code of global filters.
- The before code of controller filters.
- The before code of action method filters.
- The after code of action method filters.
- The after code of controller filters.
- The after code of global filters.



[lnk0001]: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-8.0
[lnk0002]: https://code-maze.com/filters-in-asp-net-core-mvc/
[lnk0003]: https://code-maze.com/action-filters-aspnetcore/