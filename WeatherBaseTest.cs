using dotenv.net;
using System.Text.Json;

namespace api;

public abstract class WeatherBaseTest
{
  protected readonly string _baseUrl = "https://api.weatherapi.com/v1";
  protected JsonSerializerOptions options;


  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
    options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };
    DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 3));
  }

}