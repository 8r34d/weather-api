// WeatherAstronomyFixture
// AstronomyTestDataModel

using CsvHelper;
using System.Globalization;
using api.models;

namespace api.fixtures;

public class WeatherAstronomyFixture
{
  public static IEnumerable<AstronomyTestDataModel> GetTestData()
  {
    string inputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"data/weather-astronomy-data.csv");

    using var reader = new StreamReader(inputFile);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

    var records = csv.GetRecords<AstronomyTestDataModel>();

    foreach (var record in records)
    {
      yield return record;
    }
  }
}