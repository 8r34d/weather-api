using api.enums;
using api.fixtures;
using api.helpers;
using api.models;
using dotenv.net.Utilities;
using RestSharp;

namespace api.tests;

[TestFixtureSource(typeof(AstronomyFixture), nameof(AstronomyFixture.GetTestData))]
public class AstronomyGetTest(AstronomyTestModel data) : BaseTest
{
  private readonly string _api = ApiHelper.Astronomy.Value;
  private readonly string _format = FormatHelper.Json.Value;
  private readonly AstronomyTestModel _data = data;

  [Test]
  public void GetAstronomy()
  {
    Console.WriteLine($"{this.GetType().Name}: {string.Join(",", DataHelper<AstronomyTestModel>.ExpandData(_data))}");

    if (_data.Type == TestType.Skip)
    {
      Console.WriteLine($"{this.GetType().Name}: {_data.Type}");
    }
    else
    {
      var getUrl = $"{_baseUrl}/{_api}.{_format}?q={_data.Query}&dt={_data.Date}";

      RestClient client = new(getUrl);
      client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

      RestRequest restRequest = new(getUrl, Method.Get);

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
}
