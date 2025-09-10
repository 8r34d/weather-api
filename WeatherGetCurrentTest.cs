using System.Net;
using FluentAssertions;
using RestSharp;
using dotenv.net;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

[TestFixtureSource(typeof(WeatherGetCurrentFixtureData), nameof(WeatherGetCurrentFixtureData.FixtureParams))]
public class WeatherGetCurrentTest
{

  private readonly string _query;
  private readonly string _name = "";
  private readonly bool _expectError;
  private readonly int _errorCode;
  private readonly string _errorMessage = "";
  private readonly string _baseUrl = "https://api.weatherapi.com/v1";
  private JsonSerializerOptions options;


  public WeatherGetCurrentTest(string query, string name, bool expectError)
  {
    _query = query;
    _name = name;
    _expectError = expectError;
  }
  public WeatherGetCurrentTest(string query, bool expectError, int errorCode, string errorMessage)
  {
    _query = query;
    _expectError = expectError;
    _errorCode = errorCode;
    _errorMessage = errorMessage;
  }


  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
    options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };
    DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 3));
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

  public class WeatherGetCurrentFixtureData
  {
    public static IEnumerable<TestFixtureData> FixtureParams
    {
      get
      {
        yield return new TestFixtureData("london", "London", false);
        yield return new TestFixtureData("IP1", "Ipswich", false);
        yield return new TestFixtureData("90521", true, 1006, "No matching location found.");
      }
    }
  }
}