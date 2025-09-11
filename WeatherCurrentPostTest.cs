using RestSharp;
using dotenv.net.Utilities;
using CsvHelper;
using System.Globalization;

namespace api;

[TestFixtureSource(typeof(WeatherCurrentPostFixtureData), nameof(WeatherCurrentPostFixtureData.GetTestData))]
public class WeatherCurrentPostTest : WeatherBaseTest
{
  // private readonly string _query;
  // private readonly string _name = "";
  // private readonly bool _expectError;
  // private readonly int _errorCode;
  // private readonly string _errorMessage = "";

  private readonly CurrentTestDataModel _data;


  public WeatherCurrentPostTest(CurrentTestDataModel data)
  {
    _data = data;
  }

  // public WeatherCurrentPostTest(WeatherCurrentDataValid data)
  // {
  //   Assert.That(data.ExpectError, Is.EqualTo(false),
  //   "ExpectError should be set to false for Valid Data");
  //   _query = data.Query;
  //   _name = data.Name;
  //   _expectError = data.ExpectError;
  //   ;
  // }
  // public WeatherCurrentPostTest(WeatherCurrentDataInvalid data)
  // {
  //   Assert.That(data.ExpectError, Is.EqualTo(true),
  //   "ExpectError should be set to true for Invalid Data");
  //   _query = data.Query;
  //   _expectError = data.ExpectError;
  //   _errorCode = data.ErrorCode;
  //   _errorMessage = data.ErrorMessage;
  // }

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
    // string inputFile = @"../../../test-data.csv";
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