using RestSharp;
using dotenv.net.Utilities;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentGetFixtureData), nameof(WeatherCurrentGetFixtureData.FixtureParams))]
public class WeatherCurrentGetTest : WeatherBaseTest
{
  private readonly string _query;
  private readonly string _name = "";
  private readonly bool _expectError;
  private readonly int _errorCode;
  private readonly string _errorMessage = "";

  public WeatherCurrentGetTest(WeatherCurrentDataValid data)
  {
    Assert.That(data.ExpectError, Is.EqualTo(false),
    "ExpectError should be set to false for Valid Data");
    _query = data.Query;
    _name = data.Name;
    _expectError = data.ExpectError;
  }
  public WeatherCurrentGetTest(WeatherCurrentDataInvalid data)
  {
    Assert.That(data.ExpectError, Is.EqualTo(true),
    "ExpectError should be set to true for Invalid Data");
    _query = data.Query;
    _expectError = data.ExpectError;
    _errorCode = data.ErrorCode;
    _errorMessage = data.ErrorMessage;
  }

  [Test]
  public void GetCurrent()
  {
    var format = "json";
    var name = "current";
    var getUrl = $"{_baseUrl}/v1/{name}.{format}?q={_query}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_expectError)
    {
      WeatherCurrentHelper.ErrorAssertions(restResponse, options, _errorCode, _errorMessage);
    }
    else
    {
      WeatherCurrentHelper.ContentAssertions(restResponse, options, _name);
    }
  }

  public class WeatherCurrentGetFixtureData
  {
    public static IEnumerable<TestFixtureData> FixtureParams
    {
      get
      {
        yield return new TestFixtureData(new WeatherCurrentDataValid("london", "London", false));
        yield return new TestFixtureData(new WeatherCurrentDataValid("SW5", "Earls Court", false));
        yield return new TestFixtureData(new WeatherCurrentDataInvalid("90521", true, 1006, "No matching location found."));
      }
    }
  }
}