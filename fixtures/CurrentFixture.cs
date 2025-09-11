using CsvHelper;
using System.Globalization;
using api.models;

namespace api.fixtures;

public class CurrentFixture
{
  public static IEnumerable<CurrentTestDataModel> GetTestData()
  {
    string inputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"data/current-data.csv");

    using var reader = new StreamReader(inputFile);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

    var records = csv.GetRecords<CurrentTestDataModel>();

    foreach (var record in records)
    {
      yield return record;
    }
  }
}