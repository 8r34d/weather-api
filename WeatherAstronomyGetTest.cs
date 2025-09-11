using System.Net;
using RestSharp;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

[TestFixtureSource(typeof(WeatherAstronomyGetFixtureData), nameof(WeatherAstronomyGetFixtureData.FixtureParams))]
public class WeatherAstronomyGetTest : WeatherBaseTest
{
  private readonly string _query;
  private readonly string _date;
  private readonly string _name = "";
  private readonly bool _expectError;
  private readonly int _errorCode;
  private readonly string _errorMessage = "";

  public WeatherAstronomyGetTest(WeatherAstronomyDataValid data)
  {
    Assert.That(data.ExpectError, Is.EqualTo(false),
    "ExpectError should be set to false for Valid Data");
    _query = data.Query;
    _date = data.Date;
    _name = data.Name;
    _expectError = data.ExpectError;
  }
  public WeatherAstronomyGetTest(WeatherAstronomyDataInvalid data)
  {
    Assert.That(data.ExpectError, Is.EqualTo(true),
    "ExpectError should be set to true for Invalid Data");
    _query = data.Query;
    _date = data.Date;
    _expectError = data.ExpectError;
    _errorCode = data.ErrorCode;
    _errorMessage = data.ErrorMessage;
  }

  [Test]
  public void GetAstronomy()
  {
    var format = "json";
    var name = "astronomy";
    var getUrl = $"{_baseUrl}/v1/{name}.{format}?q={_query}&dt={_date}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_expectError)
    {
      WeatherAstronomyHelper.ErrorAssertions(restResponse, options, _errorCode, _errorMessage);
    }
    else
    {
      WeatherAstronomyHelper.ContentAssertions(restResponse, options, _name);
    }
  }

  public class WeatherAstronomyGetFixtureData
  {
    public static IEnumerable<TestFixtureData> FixtureParams
    {
      get
      {
        yield return new TestFixtureData(new WeatherAstronomyDataValid("london", "2024-12-31", "London", false));
        yield return new TestFixtureData(new WeatherAstronomyDataValid("SW5", "2025-09-11", "Earls Court", false));
        yield return new TestFixtureData(new WeatherAstronomyDataInvalid("90521", "2023-11-30", true, 1006, "No matching location found."));
      }
    }
  }
}
