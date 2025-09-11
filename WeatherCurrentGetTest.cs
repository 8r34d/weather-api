using RestSharp;
using dotenv.net.Utilities;
using CsvHelper;
using System.Globalization;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentGetFixtureData), nameof(WeatherCurrentGetFixtureData.GetTestData))]
public class WeatherCurrentGetTest(CurrentTestDataModel data) : WeatherBaseTest
{

  private readonly CurrentTestDataModel _data = data;

  [Test]
  public void GetCurrent()
  {
    Console.WriteLine("\n**** TEST FIXTURE");
    Console.WriteLine($"PostCurrent: {_data.Type},{_data.Ref},{_data.Description},{_data.Query},{_data.Name},{_data.ExpectedError},{_data.ErrorCode},{_data.ErrorMessage}");
    Console.WriteLine("****");

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