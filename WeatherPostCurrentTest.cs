using System.Net;
using FluentAssertions;
using RestSharp;
using dotenv.net;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

[TestFixtureSource(typeof(WeatherPostCurrentFixtureData), nameof(WeatherPostCurrentFixtureData.FixtureParams))]
public class WeatherPostCurrentTest(string query, string name)
{

  private readonly string _query = query;
  private readonly string _name = name;
  private readonly string _baseUrl = "https://api.weatherapi.com/v1";
  private JsonSerializerOptions options;

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
    restResponse.Should().NotBeNull();
    restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    WeatherCurrent? content = JsonSerializer.Deserialize<WeatherCurrent>(restResponse.Content!, options);

    content?.Current.Should().NotBeNull();
    content?.Location.Should().NotBeNull();
    content?.Location.Name.Should().Be(_name);
  }
}

public class WeatherPostCurrentFixtureData
{
  public static IEnumerable<TestFixtureData> FixtureParams
  {
    get
    {
      yield return new TestFixtureData("berlin", "Berlin");
      yield return new TestFixtureData("M5V 3L9", "Toronto");
      yield return new TestFixtureData("90210", "Beverly Hills");
      yield return new TestFixtureData("SW5", "Earls Court");
    }
  }
}