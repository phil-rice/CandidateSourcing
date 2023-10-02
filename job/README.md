



Notes:

Tooling
```
dotnet tool install --global dotnet-aspnet-codegenerator

```


To get setup up I used
```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.16
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

For the api
```
 dotnet aspnet-codegenerator controller -name JobsController -m Job -dc JobDbContext --restWithNoViews  --relativeFolderPath Controllers
 dotnet aspnet-codegenerator controller -name QuestionsController -m Question -dc JobDbContext --restWithNoViews  --relativeFolderPath Controllers
 dotnet aspnet-codegenerator controller -name SectionTemplatesController -m SectionTemplate -dc JobDbContext --restWithNoViews  --relativeFolderPath Controllers
 
```

For the dbcontext
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

