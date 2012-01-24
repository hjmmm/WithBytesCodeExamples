# Reservations sample project #

This example presents a simplified reservation system for a restaurant implemented using ASP.net MVC 3.0 with 
Razor as the view engine. The project uses the following libraries:

- [ASP.net MVC 3](http://www.asp.net/downloads)
- [Moq (mocking framework)](http://code.google.com/p/moq/downloads/detail?name=Moq.4.0.10827.Final.zip&can=2&q=)
- [NUnit](http://launchpad.net/nunitv2/2.6/2.6.0b4/+download/NUnit-2.6.0.12017.msi)

## Currenly implemented ##

- Homepage with introduction, menu and list of configuration settings
- Business logic to add and query reservations made by ID and date
- Unit tests for the business logic code
- View to list all reservations
- View to add new reservations
- Saves the reservations to a in-memory object in the Application object
- View to check reservations by date, implemented using ajax calls that retrieve answers from the server in json format

## To be implemented ##

- Present the reservations on the list page ordered
- Validate the date in the create action
- Reservations deletion (cancelation)

## Contact ##

I can be reach at twitter [@hjmmm](http://twitter.com/hjmmm),
[email](mailto:javier@withbytes.com) or in my [blog](http://blog.withbytes.com)