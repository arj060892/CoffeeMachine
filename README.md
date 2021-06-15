# Coffee Machine
This application will dispatch coffee based on user input and is implimented based on SOLID and Design Patterns
### Features
It can dispatch 3 types of coffee that are cappuccino , regular coffee and lattee. Following are unique selection options :
- For Cappuccino user will have to option to select number of Sugar required.
- With regular coffee user can choose to have a milked or a non milked one

### Composition of different coffee
|Coffee Type|Beans|Milk|Sugar|
| ------------ | ------------ | ------------ | ------------ |
| Lattee  | 3  |1   |0   |
|  Regular Coffee | 2  |1 or 0 (user input)  |0   |
|Cappuccino|5|3|1 or 0 (user input)|

## Installation
To run the application use [Visual studio 2019](https://visualstudio.microsoft.com/downloads/ "Visual studio 2019") along with [.Net Framework 5](https://dotnet.microsoft.com/download/dotnet/5.0 "5")
- Open visual studio and build the project

#Project Architecture
The application follows **clean architecture**
Following are the buidling blocks of projects:
- Core : have all the business logic and application interaction
- Test : Covers the related test cases for the business logic
