namespace api;

public class Condition
{
  public required string Text { get; set; }
  public required string Icon { get; set; }
  public int Code { get; set; }
}

public class Current
{
  public int Last_updated_epoch { get; set; }
  public required string Last_updated { get; set; }
  public double Temp_c { get; set; }
  public double Temp_f { get; set; }
  public int Is_day { get; set; }
  public required Condition Condition { get; set; }
  public double Wind_mph { get; set; }
  public double Wind_kph { get; set; }
  public int Wind_degree { get; set; }
  public required string Wind_dir { get; set; }
  public double Pressure_mb { get; set; }
  public double Pressure_in { get; set; }
  public double Precip_mm { get; set; }
  public double Precip_in { get; set; }
  public int Humidity { get; set; }
  public int Cloud { get; set; }
  public double Feelslike_c { get; set; }
  public double Feelslike_f { get; set; }
  public double Windchill_c { get; set; }
  public double Windchill_f { get; set; }
  public double Heatindex_c { get; set; }
  public double Heatindex_f { get; set; }
  public double Dewpoint_c { get; set; }
  public double Dewpoint_f { get; set; }
  public double Vis_km { get; set; }
  public double Vis_miles { get; set; }
  public double Uv { get; set; }
  public double Gust_mph { get; set; }
  public double Gust_kph { get; set; }
  public double Short_rad { get; set; }
  public double Diff_rad { get; set; }
  public double Dni { get; set; }
  public double Gti { get; set; }
}

public class Location
{
  public required string Name { get; set; }
  public required string Region { get; set; }
  public required string Country { get; set; }
  public double Lat { get; set; }
  public double Lon { get; set; }
  public required string Tz_id { get; set; }
  public int Localtime_epoch { get; set; }
  public required string Localtime { get; set; }
}

public class WeatherCurrent
{
  public required Location Location { get; set; }
  public required Current Current { get; set; }
}
