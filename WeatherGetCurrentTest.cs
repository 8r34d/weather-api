using System.Net;
using FluentAssertions;
using RestSharp;
using dotenv.net;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentFixtureData), nameof(WeatherCurrentFixtureData.FixtureParams))]
public class WeatherTests(string query)
{

  private readonly string _query = query;
  public string baseUrl = "https://api.weatherapi.com/v1";
  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
    DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 3));
  }

  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetCurrent()
  {
    var format = "json";
    var name = "current";
    var testQuery = "new york";
    var getUrl = $"{baseUrl}/v1/{name}.{format}?q={testQuery}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    restResponse.Should().NotBeNull();
    restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    Console.WriteLine(restResponse.Content);
  }

  [Test]
  public void PostCurrent()
  {

    var format = "json";
    var name = "current";
    // var query = "Berlin";
    var postUrl = $"{baseUrl}/v1/{name}.{format}";

    RestClient client = new(postUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(postUrl, Method.Post);
    restRequest.AddParameter("q", _query);

    RestResponse restResponse = client.Execute(restRequest);
    restResponse.Should().NotBeNull();
    restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };

    WeatherCurrent? content = JsonSerializer.Deserialize<WeatherCurrent>(restResponse.Content!, options);

    content?.Current.Should().NotBeNull();
    content?.Location.Should().NotBeNull();
    content?.Location.Name.Should().Be(_query);
  }
}

public class WeatherCurrentFixtureData
{
  public static IEnumerable<TestFixtureData> FixtureParams
  {
    get
    {
      yield return new TestFixtureData("Berlin");
      yield return new TestFixtureData("Paris");
      yield return new TestFixtureData("Sydney");
    }
  }
}