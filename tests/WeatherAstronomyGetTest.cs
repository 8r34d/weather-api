using RestSharp;
using dotenv.net.Utilities;
using api.helpers;
using api.fixtures;
using api.models;

namespace api.tests;

[TestFixtureSource(typeof(AstronomyFixture), nameof(AstronomyFixture.GetTestData))]
public class WeatherAstronomyGetTest(AstronomyTestDataModel data) : WeatherBaseTest
{
  private readonly AstronomyTestDataModel _data = data;

  [Test]
  public void GetAstronomy()
  {

    Console.WriteLine($"GetAstronomy: {_data.Type},{_data.Ref},{_data.Description},{_data.Query},{_data.Date},{_data.Name},{_data.ExpectError},{_data.ErrorCode},{_data.ErrorMessage}");

    var format = "json";
    var name = "astronomy";
    var getUrl = $"{_baseUrl}/v1/{name}.{format}?q={_data.Query}&dt={_data.Date}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_data.ExpectError)
    {
      WeatherAstronomyHelper.ErrorAssertions(restResponse, options, _data);
    }
    else
    {
      WeatherAstronomyHelper.ContentAssertions(restResponse, options, _data);
    }
  }
}
