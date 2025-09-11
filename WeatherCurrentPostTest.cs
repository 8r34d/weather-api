using RestSharp;
using dotenv.net.Utilities;
using CsvHelper;
using System.Globalization;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentPostFixtureData), nameof(WeatherCurrentPostFixtureData.GetTestData))]
public class WeatherCurrentPostTest : WeatherBaseTest
{
  private readonly CurrentTestDataModel _data;


  public WeatherCurrentPostTest(CurrentTestDataModel data)
  {
    _data = data;
  }

  [Test]
  public void PostCurrent()
  {
    var format = "json";
    var name = "current";
    var postUrl = $"{_baseUrl}/v1/{name}.{format}";

    RestClient client = new(postUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(postUrl, Method.Post);
    restRequest.AddParameter("q", _data.Query);

    RestResponse restResponse = client.Execute(restRequest);
    Assert.That(restResponse.Content, Is.Not.Empty);

    if (_data.ExpectedError)
    {
      WeatherCurrentHelper.ErrorAssertions(restResponse, options, _data);
    }
    else
    {
      WeatherCurrentHelper.ContentAssertions(restResponse, options, _data);
    }
  }
}

public class WeatherCurrentPostFixtureData
{
  public static IEnumerable<TestFixtureData> FixtureParams
  {
    get
    {
      yield return new TestFixtureData(new WeatherCurrentDataInvalid("?", true, 1006, "No matching location found."));
      yield return new TestFixtureData(new WeatherCurrentDataValid("M5V 3L9", "Toronto", false));
      yield return new TestFixtureData(new WeatherCurrentDataValid("90210", "Beverly Hills", false));
    }
  }

  public static IEnumerable<CurrentTestDataModel> GetTestData()
  {
    string inputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"data/test-data.csv");

    using var reader = new StreamReader(inputFile);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

    var records = csv.GetRecords<CurrentTestDataModel>();

    foreach (var record in records)
    {
      yield return record;
    }
  }
}