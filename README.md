# Experian.CreditCards

## Project

* VS2022
* .Net6
* EFCore code first using mssqlserver - default connection using mssqllocaldb
* Other Dependencies
  * Microsoft.RulesEngine
  * Serilog
  * Swagger
  
## Setup

DB needs to be created. This can be done by running the following command in the Package Manager Console for the Experian.CreditCards project:

* update-database

Might need to run the following commands first if EF not previously used:

* Install-Package Microsoft.EntityFrameworkCore.Tools
* Update-Package Microsoft.EntityFrameworkCore.Tools

## Run

* Postman collection included in main project
* When run through VS, opens swagger
* Unit test project included


## Considerations

Production Troubleshooting - Basic Serilog rolling file logging is implemented, in a production environment better options could include monitoring such as AWS cloudwatch or similar
Validation - Request validation is achieved by .net model validation, using attributes
Unit tests - Xunit with Moq unit tests have been added to test the basic application logic avoiding redundant framework testing, further/better tests could be added to test boundaries between salary, age etc 
Improvements - Various comments in code with suggested improvments for a production version





