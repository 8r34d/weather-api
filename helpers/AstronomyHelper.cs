using System.Net;
using RestSharp;
using System.Text.Json;
using api.models;

namespace api.helpers;

public class AstronomyHelper
{
  public static void ContentAssertions(
    RestResponse restResponse,
    JsonSerializerOptions options,
    AstronomyTestModel data)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

    AstronomyModel? content = JsonSerializer.Deserialize<AstronomyModel>(restResponse.Content!, options);

    Assert.Multiple(() =>
    {
      Assert.That(content?.Location, Is.Not.Null);
      Assert.That(content?.Location.Name, Is.EqualTo(data.Name));
      Assert.That(content?.Astronomy.Astro.Is_moon_up, Is.AnyOf(0, 1));
      Assert.That(content?.Astronomy.Astro.Is_sun_up, Is.AnyOf(0, 1));
      Assert.That(content?.Astronomy.Astro.Moon_phase, Is.AnyOf(
        "New Moon",
        "Waxing Crescent",
        "First Quarter",
        "Waxing Gibbous",
        "Full Moon",
        "Waning Gibbous",
        "Last Quarter",
        "Waning Crescent"));
    });
  }

  public static void ErrorAssertions(
    RestResponse restResponse,
    JsonSerializerOptions options,
    AstronomyTestModel data)
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