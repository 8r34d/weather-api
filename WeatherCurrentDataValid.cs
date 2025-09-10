namespace api;

public record WeatherCurrentDataValid
(
  string Query,
  string Name,
  bool ExpectError
);
