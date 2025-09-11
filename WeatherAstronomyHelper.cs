using System.Net;
using RestSharp;
using System.Text.Json;

namespace api;

public class WeatherAstronomyHelper
{
  public static void ContentAssertions(
    RestResponse restResponse,
    JsonSerializerOptions options,
    string _name)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

    WeatherAstronomy? content = JsonSerializer.Deserialize<WeatherAstronomy>(restResponse.Content!, options);

    Assert.Multiple(() =>
    {
      Assert.That(content?.Location, Is.Not.Null);
      Assert.That(content?.Location.Name, Is.EqualTo(_name));
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
    int _errorCode,
    string _errorMessage)
  {
    Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

    WeatherError? weatherError = JsonSerializer.Deserialize<WeatherError>(restResponse.Content!, options);

    Assert.That(weatherError?.Error, Is.Not.Null);
    Assert.Multiple(() =>
    {
      Assert.That(weatherError?.Error.Code, Is.EqualTo(_errorCode));
      Assert.That(weatherError?.Error.Message, Is.EqualTo(_errorMessage));
    });
  }
}