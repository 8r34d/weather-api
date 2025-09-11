using System.Net;
using RestSharp;
using System.Text.Json;
using api.models;

namespace api.helpers;

public class CurrentHelper
{
  public static void ContentAssertions(
    RestResponse restResponse,
    JsonSerializerOptions options,
    CurrentTestModel data)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

    CurrentModel? content = JsonSerializer.Deserialize<CurrentModel>(restResponse.Content!, options);

    Assert.Multiple(() =>
    {
      Assert.That(content?.Current, Is.Not.Null);
      Assert.That(content?.Location, Is.Not.Null);
      Assert.That(content?.Location.Name, Is.EqualTo(data.Name));
      Assert.That(content?.Current.Is_day, Is.AnyOf(0, 1));
    });
  }

  public static void ErrorAssertions(
    RestResponse restResponse,
    JsonSerializerOptions options,
    CurrentTestModel data)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

    ErrorModel? weatherError = JsonSerializer.Deserialize<ErrorModel>(restResponse.Content!, options);

    Assert.That(weatherError?.Error, Is.Not.Null);
    Assert.Multiple(() =>
    {
      Assert.That(weatherError?.Error.Code, Is.EqualTo(data.ErrorCode));
      Assert.That(weatherError?.Error.Message, Is.EqualTo(data.ErrorMessage));
    });
  }
}