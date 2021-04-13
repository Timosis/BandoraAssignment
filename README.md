# Bondora

Appalication is implemented two separated project as client-server architecture by using .net core mvc, .net core webapi and entity framework core. 
Client is Bondora.Web and server is Bondora.WebApi.
Before starting application webapi appsettings file should be changed for migration.

### appsettings.json

```json

"ConnectionStrings": {
    "DefaultConnection": "Server = YOURSERVERNAME; Database=BondoraAppDb ;User ID=YOURUSERID; Password=YOURPASSWORD   ;TrustServerCertificate=True;Trusted_Connection=False;Connection Timeout=30;Integrated Security=False;Persist Security Info=False;Encrypt=True;MultipleActiveResultSets=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

For local Sql Server Express settings which comes with Visual Studio, appsettings.json should be as following 


### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BondoraAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

After changing appsettings.json set Bondora.Webapi project as a startup project. This helps avoiding get error while adding migration.
 - Open ``` Package Console Manager ``` and ensure that ``` DefaultProject ``` is set ```Bondora.WebApi```.
 - Write ```add-migration``` on console and enter migration name like ``` BondoraAppMigration_1 ``` or ``` Bondora_Db_Mig ```. You can enter migration name whatever you want. 
 - After completed migration write ```update-database```. This reflects our app entity design to sql server as tables. Note that after created migration writing ```update-database``` is neccessary for making changes on database server.Otherwise app database won't seen on database server.
 
 
 When above instructions are done. ``` Right click Solution'Bondora' ``` and choose ```Properties```.
 On startup page select ``` Multiple Project project ``` 
 Change ``` Bondora.Webapi ``` and  ```Bondora.Webapi actions ``` as start then apply. This is for to start both projects.
 
 At the begining there will be no data on database. Before adding customer add equipments on ```Equipments``` page.
 - Create customer on customers page.
 - After created a customer, customer informations is seen on customers page.
 - Click rent equipment button to consider customer rents. But you haven't add any rent.
 - To rent equipment for customer click ```Rent Equipment``` button.Great! Now you're in customer basket. 
 - Click ```Add Equipment``` for adding vehicle to customer basket by selecting equipment and how many days you would like to rent it.
 - When you you finished selecting equipment ```Checkout``` the basket to see how much price it costs and total in order datatable.
 - Lastly you are able to print your invoice by clicking ```Export Pdf```. There will be downloaded your invoice as a pdf document.
