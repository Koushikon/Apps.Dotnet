# Razor Web III:

@Learn from Bhrugen Patel course

- Multiple Projects for related works Like `AppModels` for Models Project, `AppDataAccess` for DataAccess Project, `AppUtility` for Useful functions. 
- Inside this Project we're accessing Database through `Repository-Pattern` without the `Unit-of-Work`.
- And the `Repository-Pattern` behind the scene is using the `ApplicationDBContext` class.
- With this we separate the Database access logic with Business Logic.
- This Implements Identity Management for Login, Signup and Authorization using `EF.Core`.
- Implements Shorpping Cart feature for product items add or remove.
- Implements Order placing
- Implements Stripe Payment Geteway as Payment system.

#### Drawback With only `Repository-Pattern`
- We have now `CategoryRepository` but in future we'll have FoodTypeRepository and Many more for every DbSet.
- With that if we want to use multiple Repositories in one Page we'll have to explicitly define every Repository we want with Dependency Injection.
- The ideal approach would be to use `Unit-of-Work` that will wrap all of the Repository inside one common umbrella.

## To Run the Project

1. Check for the connection string if its there or not.
2. Then Check for Database Shopper if it is already available delete that.
3. Here in This Project we're using `EF.Core` as out Database Service. To create the database using `EF.Core` run Command:
4. Inside Package Manager Console: `Update-Database`.
5. Add `TinyMice Api-key` to access the MenuItem Page.
6. After that run the Project.