using RestSharp;
using dotenv.net.Utilities;
using api.fixtures;
using api.models;
using api.helpers;

namespace api.tests;

[TestFixtureSource(typeof(CurrentFixture), nameof(CurrentFixture.GetTestData))]
public class CurrentGetTest(CurrentTestModel data) : BaseTest
{
  private readonly CurrentTestModel _data = data;

  [Test]
  public void GetCurrent()
  {
    var x = DataHelper<CurrentTestModel>.ExpandData(_data);
    Console.WriteLine($"{this.GetType().Name}: {string.Join(",", x)}");

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
      CurrentHelper.ErrorAssertions(restResponse, options, _data);
    }
    else
    {
      CurrentHelper.ContentAssertions(restResponse, options, _data);
    }
  }
}