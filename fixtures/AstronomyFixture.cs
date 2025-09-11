using CsvHelper;
using System.Globalization;
using api.models;

namespace api.fixtures;

public class AstronomyFixture
{
  public static IEnumerable<AstronomyTestModel> GetTestData()
  {
    string inputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"data/astronomy-data.csv");

    using var reader = new StreamReader(inputFile);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

    var records = csv.GetRecords<AstronomyTestModel>();

    foreach (var record in records)
    {
      yield return record;
    }
  }
}