namespace api.models;

public class CurrentCondition
{
  public required string Text { get; set; }
  public required string Icon { get; set; }
  public required int Code { get; set; }
}

public class CurrentCurrent
{
  public required int Last_updated_epoch { get; set; }
  public required string Last_updated { get; set; }
  public required double Temp_c { get; set; }
  public required double Temp_f { get; set; }
  public required int Is_day { get; set; }
  public required CurrentCondition Condition { get; set; }
  public required double Wind_mph { get; set; }
  public required double Wind_kph { get; set; }
  public required int Wind_degree { get; set; }
  public required string Wind_dir { get; set; }
  public required double Pressure_mb { get; set; }
  public required double Pressure_in { get; set; }
  public required double Precip_mm { get; set; }
  public required double Precip_in { get; set; }
  public required int Humidity { get; set; }
  public required int Cloud { get; set; }
  public required double Feelslike_c { get; set; }
  public required double Feelslike_f { get; set; }
  public required double Windchill_c { get; set; }
  public required double Windchill_f { get; set; }
  public required double Heatindex_c { get; set; }
  public required double Heatindex_f { get; set; }
  public required double Dewpoint_c { get; set; }
  public required double Dewpoint_f { get; set; }
  public required double Vis_km { get; set; }
  public required double Vis_miles { get; set; }
  public required double Uv { get; set; }
  public required double Gust_mph { get; set; }
  public required double Gust_kph { get; set; }
  public required double Short_rad { get; set; }
  public required double Diff_rad { get; set; }
  public required double Dni { get; set; }
  public required double Gti { get; set; }
}

public class WeatherCurrentModel
{
  public required Location Location { get; set; }
  public required CurrentCurrent Current { get; set; }
}
