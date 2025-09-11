namespace api;

public record WeatherAstronomyDataInvalid
(
  string Query,
  string Date,
  bool ExpectError,
  int ErrorCode,
  string ErrorMessage
);