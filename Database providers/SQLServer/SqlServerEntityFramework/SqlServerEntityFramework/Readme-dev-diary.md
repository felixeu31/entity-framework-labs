
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