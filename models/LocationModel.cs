namespace api.models;

public class Location
{
  public required string Name { get; set; }
  public required string Region { get; set; }
  public required string Country { get; set; }
  public required double Lat { get; set; }
  public required double Lon { get; set; }
  public required string Tz_id { get; set; }
  public required int Localtime_epoch { get; set; }
  public required string Localtime { get; set; }
}