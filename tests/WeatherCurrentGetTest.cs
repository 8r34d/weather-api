using RestSharp;
using dotenv.net.Utilities;
using api.fixtures;
using api.models;
using api.helpers;

namespace api.tests;

[TestFixtureSource(typeof(CurrentFixture), nameof(CurrentFixture.GetTestData))]
public class WeatherCurrentGetTest(CurrentTestDataModel data) : WeatherBaseTest
{
  private readonly CurrentTestDataModel _data = data;

  [Test]
  public void GetCurrent()
  {
    Console.WriteLine($"GetCurrent: {_data.Type},{_data.Ref},{_data.Description},{_data.Query},{_data.Name},{_data.ExpectError},{_data.ErrorCode},{_data.ErrorMessage}");

    var format = "json";
    var name = "current";
    var getUrl = $"{_baseUrl}/v1/{name}.{format}?q={_data.Query}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_data.ExpectError)
    {
      WeatherCurrentHelper.ErrorAssertions(restResponse, options, _data);
    }
    else
    {
      WeatherCurrentHelper.ContentAssertions(restResponse, options, _data);
    }
  }
}