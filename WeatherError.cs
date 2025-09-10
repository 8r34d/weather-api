namespace api;

public class Error
{
  public required int Code { get; set; }
  public required string Message { get; set; }
}

public class WeatherError
{
  public required Error Error { get; set; }
}