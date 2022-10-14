# Infrastructure Instructions

## Add Migrations for the ApplicationDbContext
```
Add-Migration InitialCreate -Context ApplicationDbContext -OutputDir migrations\applicationdb
update-database -context ApplicationDbContext
Remove-Migration -context ApplicationDbContext
```