using System.Net;
using FluentAssertions;
using RestSharp;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentPostFixtureData), nameof(WeatherCurrentPostFixtureData.FixtureParams))]
public class WeatherCurrentPostTest(WeatherCurrentDataValid data) : WeatherBaseTest
{

  private readonly string _query = data.Query;
  private readonly string _name = data.Name;

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

public class WeatherCurrentPostFixtureData
{
  public static IEnumerable<TestFixtureData> FixtureParams
  {
    get
    {
      yield return new TestFixtureData(new WeatherCurrentDataValid("berlin", "Berlin", false));
      yield return new TestFixtureData(new WeatherCurrentDataValid("M5V 3L9", "Toronto", false));
      yield return new TestFixtureData(new WeatherCurrentDataValid("90210", "Beverly Hills", false));
      yield return new TestFixtureData(new WeatherCurrentDataValid("SW5", "Earls Court", false));
    }
  }
}