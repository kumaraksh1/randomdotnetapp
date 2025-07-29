var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

var app = builder.Build();

const string HELLO_APP_BASE_URL = "http://localhost:5001";

// Route 1: "/"
app.MapGet("/", () =>
{
    return Results.Json(new
    {
        message = "Hello, I am just a router and testing webhook with sidecar, and blessed scenario final before release#fopefully final testingggggggg again"
    });
});

// Route 2: "/route"
app.MapGet("/route", async (IHttpClientFactory clientFactory) =>
{
    var client = clientFactory.CreateClient();

    try
    {
        var response = await client.GetAsync($"{HELLO_APP_BASE_URL}/hello");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json");
    }
    catch (HttpRequestException ex)
    {
        return Results.Json(new { error = ex.Message }, statusCode: 500);
    }
});

app.Run();