using System.Net;
using FluentAssertions;
using RestSharp;
using dotenv.net.Utilities;
using System.Text.Json;

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
    _query = data.Query;
    _name = data.Name;
    _expectError = data.ExpectError;
  }
  public WeatherCurrentGetTest(WeatherCurrentDataInvalid data)
  {
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

    if (_expectError)
    {
      restResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

      WeatherError? weatherError = JsonSerializer.Deserialize<WeatherError>(restResponse.Content!, options);

      weatherError?.Error.Should().NotBeNull();
      weatherError?.Error.Code.Should().Be(_errorCode);
      weatherError?.Error.Message.Should().Be(_errorMessage);

    }
    else
    {
      restResponse.Should().NotBeNull();
      restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

      WeatherCurrent? content = JsonSerializer.Deserialize<WeatherCurrent>(restResponse.Content!, options);

      content?.Current.Should().NotBeNull();
      content?.Location.Should().NotBeNull();
      content?.Location.Name.Should().Be(_name);
    }
  }

  public class WeatherCurrentGetFixtureData
  {
    public static IEnumerable<TestFixtureData> FixtureParams
    {
      get
      {
        yield return new TestFixtureData(new WeatherCurrentDataValid("london", "London", false));
        yield return new TestFixtureData(new WeatherCurrentDataValid("IP1", "Ipswich", false));
        yield return new TestFixtureData(new WeatherCurrentDataInvalid("90521", true, 1006, "No matching location found."));
      }
    }
  }
}