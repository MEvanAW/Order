# Order
API server in ASP.NET Core for orders of items
## Prerequisite
1. Install postgresql if you haven't. Alternatively you can use other RDBMS, but you would need to replace postgres driver with its respective relational database driver.
2. Create a database with a name of "order-api".
3. Create user secret, for example:
```
{
  "PostgreSql:ConnectionString": "Host=localhost;Port=5432;Database=order-api;Username=postgres;Password=your-password"
}
```
ASP.NET Core user secret guide: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#manage-user-secrets-with-visual-studio
### Order API Swagger
The Swagger specification defines a set of files required to describe such an API. These files can then be used by the Swagger-UI project to display the API and Swagger-Codegen to generate clients in various languages. Additional utilities can also take advantage of the resulting files, such as testing tools.<br/>Swagger is available for this API server. To open swagger UI, open https://localhost:7208/swagger/index.html or https://localhost:5208/swagger/index.html.
![OrderSwagger](https://user-images.githubusercontent.com/50491841/200461525-35732572-1c1a-4020-a25e-a31271c5cd3a.png)
