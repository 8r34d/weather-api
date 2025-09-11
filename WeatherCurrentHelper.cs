using System.Net;
using RestSharp;
using System.Text.Json;

namespace api;

public class WeatherCurrentHelper
{
  public static void ContentAssertions(
    RestResponse restResponse,
    JsonSerializerOptions options,
    CurrentTestDataModel data)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

    WeatherCurrent? content = JsonSerializer.Deserialize<WeatherCurrent>(restResponse.Content!, options);

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
    CurrentTestDataModel data)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

    WeatherError? weatherError = JsonSerializer.Deserialize<WeatherError>(restResponse.Content!, options);

    Assert.That(weatherError?.Error, Is.Not.Null);
    Assert.Multiple(() =>
    {
      Assert.That(weatherError?.Error.Code, Is.EqualTo(data.ErrorCode));
      Assert.That(weatherError?.Error.Message, Is.EqualTo(data.ErrorMessage));
    });
  }
}