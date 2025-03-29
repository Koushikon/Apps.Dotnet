# Role & Policy Based Authorization

- [Source][lnk0002] | [MS Docs][lnk0001]

We generally use the username and the password for login and based on that we are providing access to the application, but in this case, the user can access all the resources of the application. By using `Policy-based` & `Role-based` Authorization process, we can provide access to particular area of application to the user based on the `Role/Policy` of the user.

This Project implements Authorization with `Roles Based` and `Policy Based` to determine the accessabilit level of the User and only give access to ther right user.

[lnk0001]: https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-8.0
[lnk0002]: https://www.c-sharpcorner.com/article/policy-based-role-based-authorization-in-asp-net-core/