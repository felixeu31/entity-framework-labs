
# Adding migrations

```<language>
PM> Enable-Migrations
Checking if the context targets an existing database...

PM> Add-Migration FirstMigration
Scaffolding migration 'FirstMigration'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration FirstMigration' again.

PM> Update-Database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [202210020802469_FirstMigration].
Applying explicit migration: 202210020802469_FirstMigration.
Running Seed method.
```

Db is created by default in LocalDb
https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database#wheres-my-data
(localdb)\MSSQLLocalDB

Change DbContext to pass connection string

```<language>
PM> Update-Database -Verbose
System.Data.SqlClient.SqlException (0x80131904): Error relacionado con la red o específico de la instancia mientras se establecía una conexión con el servidor SQL Server. No se encontró el servidor o éste no estaba accesible. Compruebe que el nombre de la instancia es correcto y que SQL Server está configurado para admitir conexiones remotas. (provider: Named Pipes Provider, error: 40 - No se pudo abrir una conexión con SQL Server) ---> System.ComponentModel.Win32Exception (0x80004005): No se ha encontrado la ruta de acceso de la red
```