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
- Restrict Deletion if entity has any child entity.
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

## Error Message Standard
- "Không tìm thấy câu hỏi với id: {id}"
- "An unexpected error occured"

## Restore remote SQLServer 2019 Docker Linux
1. Upload .bak file to /root/srms
2. Copy .bak file to var/opt/mssql/data
```bash
docker cp /root/srms/Student-Results-Db.bak sqlserver:/var/opt/mssql/data
```
3. Open ssms
4. Click Restore database
5. Click File
6. Restore Database as: /var/opt/mssql/data/Student-Results-Db.mdf
7. Restore Database as: /var/opt/mssql/data/Student-Results-Db_log.ldf