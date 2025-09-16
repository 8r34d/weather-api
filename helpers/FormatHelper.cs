namespace api.helpers;

public class FormatHelper
{
  private FormatHelper(string value) { Value = value; }

  public string Value { get; private set; }

  public static FormatHelper Json { get { return new FormatHelper("json"); } }
  public static FormatHelper Xml { get { return new FormatHelper("xml"); } }

  public override string ToString()
  {
    return Value;
  }
}

/*
  https://www.weatherapi.com/docs/

  API	                    API Method
  Current weather	        /current.json or /current.xml
  Forecast	              /forecast.json or /forecast.xml
  Search or Autocomplete	/search.json or /search.xml
  History	                /history.json or /history.xml
  Alerts	                /alerts.json or /alerts.xml
  Marine	                /marine.json or /marine.xml
  Future	                /future.json or /future.xml
  Time Zone	              /timezone.json or /timezone.xml
  Sports	                /sports.json or /sports.xml
  Astronomy	              /astronomy.json or /astronomy.xml
  IP Lookup	              /ip.json or /ip.xml
*/