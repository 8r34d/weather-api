using api.enums;
using api.fixtures;
using api.helpers;
using api.models;
using dotenv.net.Utilities;
using RestSharp;

namespace api.tests;

[TestFixtureSource(typeof(CurrentFixture), nameof(CurrentFixture.GetTestData))]
public class CurrentGetTest(CurrentTestModel data) : BaseTest
{
  private readonly string _api = ApiHelper.Current.Value;
  private readonly string _format = FormatHelper.Json.Value;
  private readonly CurrentTestModel _data = data;

  [Test]
  public void GetCurrent()
  {
    Console.WriteLine($"{this.GetType().Name}: {string.Join(",", DataHelper<CurrentTestModel>.ExpandData(_data))}");

    if (_data.Type == TestType.Skip)
    {
      Console.WriteLine($"{this.GetType().Name}: {_data.Type}");
    }
    else
    {
      var getUrl = $"{_baseUrl}/{_api}.{_format}?q={_data.Query}";

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
}