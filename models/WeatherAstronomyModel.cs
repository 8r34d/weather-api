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

public class WeatherAstronomyModel
{
  public required Location Location { get; set; }
  public required AstronomyAstronomy Astronomy { get; set; }
}
