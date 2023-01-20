# DroneDelivery

## Walk Through Solution 
The provided solution starts by sorting the locations by weight in ascending order and the drones are sorted in descending order by weight and converted into a data structure called a queue.
Then the first drone is obtained from the queue which will be the most weight capacity, with this drone, a greedy algorithm that maximizes the number of packages delivered on each trip concerning weight will select the best trip and sum each weight.

If the sum is lower or equal to the second drone with the most weight capacity, this will be assigned to the second drone and it will come out from the queue. 
The process mentioned before will be repeated until no location remains by assignment.

## Technical Dependencies and Libraries 

The solution created with **Visual Studio 2022** consists in three **.NET 6** projects built which are the following:

* **DroneDelivery:** It has a domain driven design (DDD) architecture folder structure:   
  1. **Domain:** Folder with the application entities.
  2. **Common:** Only contains a class (DataFormatter.cs) with its interface to process input and output for the application.
  3. **Application:** Contains classes with its interfaces with business logic to find best trip.
  * **NuGet Dependencies:** None.

* **DroneDelivery.Console:** Console app where the dependencies are injected by IHost container in the Startup.cs file. 
Command usage can be obtained executed the following:
  ``drone-delivery -h`` or `drone-delivery --help`
  
  ![image](https://user-images.githubusercontent.com/16918921/213615946-a2006339-140a-4b71-b287-e5874023b404.png)

  Also it has a parameterless execution where it will ask you for the input file path.
  
  The output will print out and an output file gonna be created with the date time execution append with the file name. If OS execution platform is Windows, automatically it will open the file with `Notepad`
  
  ![image](https://user-images.githubusercontent.com/16918921/213621133-bcbbb662-29a6-4636-88c3-98323d691cf9.png)

  * **NuGet Dependencies:** 
    * Microsoft.Extensions.Hosting (7.0.0)

* **DroneDelivery.Test:** Project with four unit test for the class `DeliveryService` and `DataFormatter`

![image](https://user-images.githubusercontent.com/16918921/213621723-0dda5002-3ab4-4105-8978-c8ff94c68154.png)

In the command line, you can run the following `dotnet test DroneDelivery.Test`

  * **NuGet Dependencies:** 
    * coverlet.collector (3.1.2)
    * FluentAssertions (6.9.0)
    * xunit (2.4.1)
    * xunit.runner.visualstudio (2.4.3)
    * Microsoft.NET.Test.Sdk (17.1.0)
