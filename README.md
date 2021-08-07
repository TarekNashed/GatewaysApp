====> Backend GatewayApp.
-In the # appsettings.json file in API layer all we need to change the server name # Server=(localdb)\\MSSQLLocalDB; to be Server=(your server name);
-Run the project and the db will be genrated automaticly and no need to run add-migration or update-datebase command.
-Go to this link # https://localhost:44320/Swagger/index.html after the running the backend app and you will have all the bussiness methologies and you can test it insteed of Postman.

-This solution project contains 3 layesr for the business and one project for UnitTesting project.
3 projects:- 
 1:- NetworkData: contains DBContext, models files , Unitofwork, Repository interfaces and it's implementations.
 2:- NetworkDomain: contains IBusinessData folder which contains the IBusiness interfaces, BusinessData folder which contains the implementation of IBusinessData folder.
 3:- API: which contain the controller and extensions folder which contains the ConfigureServiceExtensions.cs.
 
4: Unit testing project.

Please contact with me if you have any issue📧
Tareqnashed171993@gmail.com

