using System.Net;
using RestSharp;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;




public class WeatherAstronomyGetTest : WeatherBaseTest
{
  [Test]
  public void GetAstronomy()
  {
    // var format = "json";
    // var name = "astronomy";
    // var date = "2025-09-10"; // DateTime to string?
    // var getUrl = $"{_baseUrl}/v1/{name}.{format}?q={_query}&dt={_date}";

    // 'https://api.weatherapi.com/v1/astronomy.json?q=SW5&dt=2025-09-10'

  }
}


// WeatherAstronomy content = JsonConvert.DeserializeObject<WeatherAstronomy>(restResponse.Content!, options);