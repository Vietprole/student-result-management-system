# Student-Result-Management-System
## Note
1. Add Default connection string to appsettings.json:
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=VT-PGH;Database=Student-Results-Db;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
2. Entity Framework command in Package Manager Console:
```cmd
get-help entityframework
```

3. Run the app:
```cmd
dotnet watch run
```

4. Migrate database:
```cmd
dotnet ef migrations add AddSomething
```

5. Update database:
```cmd
dotnet ef database update
```

6. Remove recent migration (before update database):
```cmd
dotnet ef migrations remove
```

7. Modal classes foreign key naming convention:
- E.g.: SinhvienId  
[conventions](https://learn.microsoft.com/en-us/ef/core/modeling/relationships/conventions)