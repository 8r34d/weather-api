using System.Net;
using FluentAssertions;
using RestSharp;
using dotenv.net;
using dotenv.net.Utilities;
using System.Text.Json;

namespace api;

public class WeatherTests
{
  public string baseUrl = "https://api.weatherapi.com/v1";
  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
    DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 3));
  }

  [SetUp]
  public void Setup()
  {
    // DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 3));
    // DotEnv.Load(options: new DotEnvOptions(envFilePaths: ["/Users/dean/workspace/dean/dotnet/api/.env"]));
    // DotEnv.Load(options: new DotEnvOptions(envFilePaths: ["../../../.env"]));
  }

  [Test]
  public void GetCurrent()
  {
    // dotnet test --filter Name~GetCurrent

    // https://github.com/bolorundurowb/dotenv.net
    // Search for .env Files in Parent Directories:
    // DotEnv.Load(options: new DotEnvOptions(probeForEnv: true, probeLevelsToSearch: 3));

    // var AssemblyDirectory = TestContext.CurrentContext.TestDirectory;
    // Console.WriteLine(AssemblyDirectory);
    // /Users/dean/workspace/dean/dotnet/api/bin/Debug/net9.0

    // DotEnv.Load(options: new DotEnvOptions(envFilePaths: ["../../../.env"]));

    // var key = EnvReader.GetStringValue("WEATHER_API_KEY");
    // Console.WriteLine($"WEATHER_API_KEY: {key}");

    // JSON and XML are both data serialization formats

    var format = "json";
    var name = "current";
    var query = "new york";
    var getUrl = $"{baseUrl}/v1/{name}.{format}?q={query}";

    RestClient client = new(getUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(getUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    restResponse.Should().NotBeNull();
    restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    Console.WriteLine(restResponse.Content);
  }

  [Test]
  public void PostCurrent()
  {
    // dotnet test --filter Name~PostCurrent

    var format = "json";
    var name = "current";
    var query = "Berlin";
    var postUrl = $"{baseUrl}/v1/{name}.{format}";

    // var baseUrl = "https://api.weatherapi.com/v1/current.json";

    RestClient client = new(postUrl);
    client.AddDefaultHeader("key", EnvReader.GetStringValue("WEATHER_API_KEY"));

    RestRequest restRequest = new(postUrl, Method.Post);
    restRequest.AddParameter("q", query);

    RestResponse restResponse = client.Execute(restRequest);
    restResponse.Should().NotBeNull();
    restResponse.StatusCode.Should().Be(HttpStatusCode.OK);



    // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/character-casing


    // how to cache and reuse the instance?
    // base class settings?
    // other options?
    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };

    WeatherCurrent? content = JsonSerializer.Deserialize<WeatherCurrent>(restResponse.Content!, options);

    /*

    if I add

      public required string bogus { get; set; }

    to WeatherCurrent

    as the response will never return that property I will get the following error

      System.Text.Json.JsonException : JSON deserialization for type 'api.WeatherCurrent' was missing required properties including: 'bogus'.

      any properties that are set as required will be validated!

    */

    // https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html

    // https://fluentassertions.com/introduction

    content?.Current.Should().NotBeNull();
    content?.Location.Should().NotBeNull();
    content?.Location.Name.Should().Be(query);

    // Console.WriteLine(restResponse.Content);
  }
}