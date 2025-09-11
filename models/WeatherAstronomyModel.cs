namespace api.models;

public class AstronomyAstro
{
  public required string Sunrise { get; set; }
  public required string Sunset { get; set; }
  public required string Moonrise { get; set; }
  public required string Moonset { get; set; }
  public required string Moon_phase { get; set; }
  public required int Moon_illumination { get; set; }
  public required int Is_moon_up { get; set; }
  public required int Is_sun_up { get; set; }
}

public class AstronomyAstronomy
{
  public required AstronomyAstro Astro { get; set; }
}

public class AstronomyLocation
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

public class WeatherAstronomyModel
{
  public required AstronomyLocation Location { get; set; }
  public required AstronomyAstronomy Astronomy { get; set; }
}
