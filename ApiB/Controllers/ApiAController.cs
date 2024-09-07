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

    // [HttpGet("consume-api-a")]
    // public async Task<IActionResult> ConsumeApiA()
    // {
    //     try
    //     {
    //         var response = await _httpClient.GetAsync("http://localhost:5245/simple/hello");
    //         response.EnsureSuccessStatusCode();

    //         var content = await response.Content.ReadAsStringAsync();
    //         return Ok(content);
    //     }
    //     catch (HttpRequestException ex)
    //     {
    //         return StatusCode(500, $"Error calling API A: {ex.Message}");
    //     }
    // }

    [HttpGet("consume-api-a")]
    public async Task<IActionResult> ConsumeApiA()
    {
        int maxRetries = 3;
        int delay = 3000; // 3 segundos

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                Console.WriteLine($"Intento {i + 1} de {maxRetries} para llamar a API A.");
                
                var response = await _httpClient.GetAsync("http://localhost:5245/simple/hello");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Fallo en el intento {i + 1}: {ex.Message}");

                if (i == maxRetries - 1)
                {
                    Console.WriteLine("API B: Se agotaron todos los intentos para llamar a API A.");
                    return StatusCode(500, $"Error calling API A after {maxRetries} attempts: {ex.Message}");
                }

                await Task.Delay(delay); // Espera antes de reintentar
            }
        }

        return StatusCode(500, "Failed to call API A");
    }
}
