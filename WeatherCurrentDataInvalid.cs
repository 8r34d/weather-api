namespace api;

public record WeatherCurrentDataInvalid
(
  string Query,
  bool ExpectError,
  int ErrorCode,
  string ErrorMessage
);
