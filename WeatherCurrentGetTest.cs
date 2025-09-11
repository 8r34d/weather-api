using RestSharp;
using dotenv.net.Utilities;
using CsvHelper;
using System.Globalization;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentGetFixtureData), nameof(WeatherCurrentGetFixtureData.GetTestData))]
public class WeatherCurrentGetTest : WeatherBaseTest
{

  private readonly CurrentTestDataModel _data;

  public WeatherCurrentGetTest(CurrentTestDataModel data)
  {
    _data = data;
  }

  [Test]
  public void GetCurrent()
  {
    var format = "json";
    var name = "current";
    var getUrl = $"{_baseUrl}/v1/{name}.{format}?q={_data.Query}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

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

  public class WeatherCurrentGetFixtureData
  {
    public static IEnumerable<TestFixtureData> FixtureParams
    {
      get
      {
        yield return new TestFixtureData(new WeatherCurrentDataValid("london", "London", false));
        yield return new TestFixtureData(new WeatherCurrentDataValid("SW5", "Earls Court", false));
        yield return new TestFixtureData(new WeatherCurrentDataInvalid("90521", true, 1006, "No matching location found."));
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
}