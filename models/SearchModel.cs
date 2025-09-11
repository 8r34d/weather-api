namespace api.models;

public class SearchModel
{
  public required int Id { get; set; }
  public required string Name { get; set; }
  public required string Region { get; set; }
  public required string Country { get; set; }
  public required double Lat { get; set; }
  public required double Lon { get; set; }
  public required string Url { get; set; }
}
