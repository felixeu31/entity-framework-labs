

## Initial config
- Create project (xUnit Test Template, .NET 6)
- Write basic test insert and get
- Create data model
- Install EF
	- Install-Package Microsoft.EntityFrameworkCore.SqlServer, Install-Package Microsoft.EntityFrameworkCore.Tools
- Create DbContext
	- Add configuration
- Run migrations 
	- Add-Migration
	- Update-Database

## Test for 1-N Relation duplicate 
- Add new Book with duplicate PK to author (while book with same PK already tracked)
- Add new Book with duplicate PK to DB