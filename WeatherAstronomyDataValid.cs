namespace api;

public record WeatherAstronomyDataValid
(
  string Query,
  string Date,
  string Name,
  bool ExpectError
);