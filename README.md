# Student-Result-Management-System
## How to run
1. Download & Install .NET SDK ver 8.0
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

2. Download & Install SQL Server 2022
https://www.microsoft.com/en-us/sql-server/sql-server-downloads

3. Restore packages:
```cmd
dotnet restore
```

4. Restore tools:
```cmd
dotnet tool restore
```

5. Update database:
```cmd
dotnet ef database update
```

6. Run the app:
```cmd
dotnet watch run
```

## Note (Not required)
1. Change Default connection string to appsettings.json:
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

3. Migrate database:
```cmd
dotnet ef migrations add AddSomething
```

4. Remove recent migration (before update database):
```cmd
dotnet ef migrations remove
```

5. Modal classes foreign key naming convention:
- E.g.: SinhvienId  
[conventions](https://learn.microsoft.com/en-us/ef/core/modeling/relationships/conventions)

## Github Action is triggers when push to master!

## Delete action consequences
- Delete Khoa, Nganh **WILL NOT** delete CTDT, HocPhan. Instead setting null.
- Delete CTDT **WILL** delete its PLOs, so be cautious.
- Delete HocPhan **WILL** delete LopHocPhan, so be cautious.
- Delete CLO is set to ClientCascade to CauHoi, so make sure to **include CauHoi** whenever you delete CLO or it will cause error.
```C#
var clo = await _context.CLOs
    .Include(c => c.CauHois)
    .FirstOrDefaultAsync(c => c.Id == cloId);

if (clo != null)
{
    _context.CLOs.Remove(clo);
    await _context.SaveChangesAsync();
}
```

## SQLServer Docker Query
```bash
docker exec -it sqlserver "bash"
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Viet@123456 -C
```
