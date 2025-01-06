using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ApiAController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ApiAController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("consume-api-a")]
    public async Task<IActionResult> ConsumeApiA()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://api-a-service:8080/simple/hello");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error calling API A: {ex.Message}");
        }
    }
}
