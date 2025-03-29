# Dotnet Data Transfer Object (DTOs) Demo

[Source][lnk0001]

This `Net_DTOs` project implements `JWT authentication` and use `Data Transfer Object (DTOs)` to Handle User requests. Use model Mappers to map user facing models into server models.

## Using These Nugget Packages
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.AspNetCore.Mvc.NewtonsoftJson` needed but can be used `System.Text.Json` instead.
- `Microsoft.AspNetCore.OpenApi`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`


## To Run This Project

1. Install this Nugget Package at Global level
```Powershell
dotnet tool install --global dotnet-ef
```

2. Then on Powershell Navigate to the Project folder and run this command:
```PowerShell
// To Create the Database on the provided DB server
dotnet ef migrations add <Migration_Name>
```

3. Configure the Connection String Then run this command:
```
dotnet ef database update
```

[lnk0001]: https://github.com/teddysmithdev/FinShark