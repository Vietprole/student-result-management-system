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

4. Update database:
```cmd
dotnet ef database update
```