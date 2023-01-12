# Infrastructure Instructions

## Add Migrations for the ApplicationDbContext
```
switch project to 'src\core\Infrastructure' in Package Manager Console
Add-Migration InitialCreate -Context ApplicationDbContext -OutputDir migrations\applicationdb
update-database -context ApplicationDbContext
Remove-Migration -context ApplicationDbContext
```


## Add Migrations for the SecurityDbContext
```
switch project to 'src\core\Infrastructure' in Package Manager Console
Add-Migration InitialCreate -Context SecurityDbContext -OutputDir migrations\securitydb
update-database -context SecurityDbContext
Remove-Migration -context SecurityDbContext
```