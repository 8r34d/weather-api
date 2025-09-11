using RestSharp;
using dotenv.net.Utilities;
using api.fixtures;
using api.models;
using api.helpers;

namespace api.tests;

[TestFixtureSource(typeof(CurrentFixture), nameof(CurrentFixture.GetTestData))]
public class WeatherCurrentPostTest(CurrentTestDataModel data) : WeatherBaseTest
{
  private readonly CurrentTestDataModel _data = data;

  [Test]
  public void PostCurrent()
  {
    Console.WriteLine($"PostCurrent: {_data.Type},{_data.Ref},{_data.Description},{_data.Query},{_data.Name},{_data.ExpectError},{_data.ErrorCode},{_data.ErrorMessage}");

    var format = "json";
    var name = "current";
    var postUrl = $"{_baseUrl}/v1/{name}.{format}";

    RestClient client = new(postUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(postUrl, Method.Post);
    restRequest.AddParameter("q", _data.Query);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_data.ExpectError)
    {
      CurrentHelper.ErrorAssertions(restResponse, options, _data);
    }
    else
    {
      CurrentHelper.ContentAssertions(restResponse, options, _data);
    }
  }
}