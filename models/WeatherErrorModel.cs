namespace api.models;

public class Error
{
  public required int Code { get; set; }
  public required string Message { get; set; }
}

public class WeatherErrorModel
{
  public required Error Error { get; set; }
}