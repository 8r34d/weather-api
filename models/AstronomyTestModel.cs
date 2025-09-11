using api.types;

namespace api.models;

public class AstronomyTestModel
{
  public required TestType Type { get; set; }
  public required string Ref { get; set; }
  public required string Description { get; set; }
  public required string Query { get; set; }
  public required string Date { get; set; }
  public required string Name { get; set; }
  public required bool ExpectError { get; set; }
  public required int ErrorCode { get; set; }
  public required string ErrorMessage { get; set; }
}
