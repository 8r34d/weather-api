using System.Net;
using FluentAssertions;
using RestSharp;

namespace api;

public class TodoTests
{
  [SetUp]
  public void Setup()
  {
  }

  // https://dev.to/m4rri4nne/nunit-and-c-tutorial-to-automate-your-api-tests-from-scratch-24nf

  [Test]
  public void GetTodos()
  {
    var baseUrl = "https://jsonplaceholder.typicode.com/todos/1";

    // RestClient client = new RestClient(baseUrl);
    RestClient client = new(baseUrl);

    // RestRequest restRequest = new RestRequest(baseUrl, Method.Get);
    RestRequest restRequest = new(baseUrl, Method.Get);

    RestResponse restResponse = client.Execute(restRequest);
    restResponse.Should().NotBeNull();
    restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    Console.WriteLine(restResponse.Content);
  }
}
