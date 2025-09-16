using api.enums;
using api.fixtures;
using api.helpers;
using api.models;
using dotenv.net.Utilities;
using RestSharp;

namespace api.tests;

[TestFixtureSource(typeof(CurrentFixture), nameof(CurrentFixture.GetTestData))]
public class CurrentPostTest(CurrentTestModel data) : BaseTest
{
  private readonly string _api = ApiHelper.Current.Value;
  private readonly string _format = FormatHelper.Json.Value;
  private readonly CurrentTestModel _data = data;

  [Test]
  public void PostCurrent()
  {
    Console.WriteLine($"{this.GetType().Name}: {string.Join(",", DataHelper<CurrentTestModel>.ExpandData(_data))}");

    if (_data.Type == TestType.Skip)
    {
      Console.WriteLine($"{this.GetType().Name}: {_data.Type}");
    }
    else
    {
      var postUrl = $"{_baseUrl}/{_api}.{_format}";

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
}