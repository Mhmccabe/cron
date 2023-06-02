# cron parser dev task

## purpose
coding example that will read a cron expression and print a table of results, showing the valid valies for each field type

valid paramerter types
| type          | example     | description                    |
| --------------|-------------|--------------------------------|
|literal        | 1           | individual value               |
|comma seperted | 1, 2, 3     | comma seperated list of values |
|range          | 1-5         | range of values                |
|all valus      | *           | all valid values               |
|calculate      | * / 2       | caluclated value interval of 2 |


## tech stack
tech stack is .net core v6 and Xunit for testing

## prerequsits
| task              | documentation   |
|-------------------|-----------------|
|.net core   | [instructions Here](https://docs.microsoft.com/en-us/dotnet/core/install/linux) |
|vscode| [here](https://code.visualstudio.com/docs/setup/linux) |



## solution structure

|  Project          | description / purpose            |
|-------------------|----------------------------------|
| ./cronParser/     | assembly containing the logic for the parser|
| ./cronParser.App/ | console applicaion using the cronLibrary| 
| ./cronParser.Test/| Xunit tests to ensure the tool is working|


### command line
```bash
 dotnet run "[1] [2] [3] [4] [5]" 

e.g.
run ./code every min, every day

 dotnet run "* * * * * ./code"  

run every 15min at midnight, on the 1st and 15th, every month, Mon-Friday

 dotnet run "*/15 0 1,15 * 1-5 ./code"  

```
| # |column         | description        |
| - |---------------|--------------------|
| [1] |min          | minute in the hour |
| [2] |hour         | hour in the day    |
| [3] |Day of month | day in the month   |
| [4] |month        | month in the year  |
| [5] |command      | day in the week    |



### example execution
```bash

dotnet run "*/15 0 1,15 * 1-5 ./code"

minute              0, 15, 30, 45
Hour                0
Day of month        1, 15
Month               1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
Day of week         1, 2, 3, 4, 5
Command             ./code
```
## build and run the code
### build and run
``` bash
dotnet run "*/15 0 1,15 * 1-5 ./code"  
```

### build 
``` bash
dotnet build   
```
### build and package solution
``` bash
dotnet publish -c Release 
```


# improvements
* create chain of responsability for determining parameter type
* add validation and better error message to invalid cron specification
* derive from from ParamType making specialisation for Range, All, List etc replacing switch statement in ParamType.cs::CronParamToValues() 


