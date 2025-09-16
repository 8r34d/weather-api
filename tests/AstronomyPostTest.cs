using RestSharp;
using dotenv.net.Utilities;
using api.helpers;
using api.fixtures;
using api.models;

namespace api.tests;

[TestFixtureSource(typeof(AstronomyFixture), nameof(AstronomyFixture.GetTestData))]
public class AstronomyPostTest(AstronomyTestModel data) : BaseTest
{
  private readonly AstronomyTestModel _data = data;

  [Test]
  public void PostAstronomy()
  {
    var c = this.GetType().Name;
    var x = DataHelper<AstronomyTestModel>.ExpandData(_data);
    Console.WriteLine($"{c}: {string.Join(",", x)}");

    var format = "json";
    var name = "astronomy";
    var postUrl = $"{_baseUrl}/v1/{name}.{format}";

    RestClient client = new(postUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(postUrl, Method.Post);
    restRequest.AddParameter("q", _data.Query);
    restRequest.AddParameter("dt", _data.Date);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_data.ExpectError)
    {
      AstronomyHelper.ErrorAssertions(restResponse, options, _data);
    }
    else
    {
      AstronomyHelper.ContentAssertions(restResponse, options, _data);
    }
  }
}
