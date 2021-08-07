====> Backend GatewayApp.
-In the #appsettings.json file in the API layer all we need to change the server name # Server=(localdb)\\MSSQLLocalDB; to be Server=(your server name);
-Run the project and the DB will be generated automaticly and no needs to run add-migration or update-datebase command.
-After running the project, please write /Swagger/index.html after the project link on the browser as link # https://localhost:44320/Swagger/index.html after the running the backend app and you will have all the bussiness methologies and you can test it, instead of Postman.

-This solution project contains 3 layers for the business and one project for the UnitTesting project.
3 projects:- 
 1:- NetworkData: contains DBContext, models files, Unitofwork, Repository interfaces, and its implementations.
 2:- NetworkDomain: contains IBusinessData folder which contains the IBusinessData interfaces, BusinessData folder which contains the implementation of IBusinessData folder.
 3:- API: which contains the controller and extensions folder which contains the ConfigureServiceExtensions.cs.
 
4: Unit testing project.

#Please contact me if you have any comments 📧
Tareqnashed171993@gmail.com

