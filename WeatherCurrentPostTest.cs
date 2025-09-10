using System.Net;
using RestSharp;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentPostFixtureData), nameof(WeatherCurrentPostFixtureData.FixtureParams))]
public class WeatherCurrentPostTest : WeatherBaseTest
{
  private readonly string _query;
  private readonly string _name = "";
  private readonly bool _expectError;
  private readonly int _errorCode;
  private readonly string _errorMessage = "";

  public WeatherCurrentPostTest(WeatherCurrentDataValid data)
  {
    _query = data.Query;
    _name = data.Name;
    _expectError = data.ExpectError;
  }
  public WeatherCurrentPostTest(WeatherCurrentDataInvalid data)
  {
    _query = data.Query;
    _expectError = data.ExpectError;
    _errorCode = data.ErrorCode;
    _errorMessage = data.ErrorMessage;
  }

  [Test]
  public void PostCurrent()
  {
    var format = "json";
    var name = "current";
    var postUrl = $"{_baseUrl}/v1/{name}.{format}";

    RestClient client = new(postUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(postUrl, Method.Post);
    restRequest.AddParameter("q", _query);

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
}

public class WeatherCurrentPostFixtureData
{
  public static IEnumerable<TestFixtureData> FixtureParams
  {
    get
    {
      yield return new TestFixtureData(new WeatherCurrentDataInvalid("?", true, 1006, "No matching location found."));
      yield return new TestFixtureData(new WeatherCurrentDataValid("M5V 3L9", "Toronto", false));
      yield return new TestFixtureData(new WeatherCurrentDataValid("90210", "Beverly Hills", false));
    }
  }
}