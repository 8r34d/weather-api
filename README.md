# weather-api

Automated testing of API

- NUnit

  - https://nunit.org/

- RestSharp

  - https://restsharp.dev/

- CsvHelper

  - https://joshclose.github.io/CsvHelper/

## API - Test Application

- https://www.weatherapi.com/

### Pricing and Registration

- https://www.weatherapi.com/pricing.aspx

- https://www.weatherapi.com/signup.aspx

### Credentials

Create a `.env` file

See `example-dot-env`

## Automated Tests

_run the tests_

```
dotnet test
```

### Filters

- `dotnet test --filter <Expression>`

```
dotnet test --filter Astronomy
```

- matches:

  - AstronomyGetTest

  - AstronomyPostTest

```
dotnet test --filter Get
```

- matches:

  - AstronomyGetTest

  - CurrentGetTest

## Test Logs

### AstronomyGetTest

```
AstronomyGetTest: Valid,ASTRONOMY_DATA_0001,City name,london,2022-07-22,London,False,0,None
AstronomyGetTest: Valid,ASTRONOMY_DATA_0002,UK postcode,SW5,2023-08-23,Earls Court,False,0,None
AstronomyGetTest: Invalid,ASTRONOMY_DATA_0003,Bad US zip,90521,2024-09-24,None,True,1006,No matching location found.
...
```

### AstronomyPostTest

```
...
AstronomyPostTest: Valid,ASTRONOMY_DATA_0004,US zip,90210,2021-12-25,Beverly Hills,False,0,None
AstronomyPostTest: Valid,ASTRONOMY_DATA_0005,Canada postal code,M5V 3L9,2020-01-26,Toronto,False,0,None
AstronomyPostTest: Invalid,ASTRONOMY_DATA_0006,Bad input,?,2019-02-27,None,True,1006,No matching location found.
```

### CurrentGetTest

```
CurrentGetTest: Valid,CURRENT_DATA_0001,City name,london,London,False,0,None
CurrentGetTest: Valid,CURRENT_DATA_0002,UK postcode,SW5,Earls Court,False,0,None
CurrentGetTest: Invalid,CURRENT_DATA_0003,Bad US zip,90521,None,True,1006,No matching location found.
...
```

### CurrentPostTest

```
...
CurrentPostTest: Valid,CURRENT_DATA_0004,US zip,90210,Beverly Hills,False,0,None
CurrentPostTest: Valid,CURRENT_DATA_0005,Canada postal code,M5V 3L9,Toronto,False,0,None
CurrentPostTest: Invalid,CURRENT_DATA_0006,Bad input,?,None,True,1006,No matching location found.
```
