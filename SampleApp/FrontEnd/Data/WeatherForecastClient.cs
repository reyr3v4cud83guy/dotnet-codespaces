namespace FrontEnd.Data;

public class WeatherForecastClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherForecastClient> _logger;

    // Constructor to initialize HttpClient and ILogger
    public WeatherForecastClient(HttpClient httpClient, ILogger<WeatherForecastClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    // Method to fetch weather forecast data
    public async Task<WeatherForecast[]> GetForecastAsync(DateTime? startDate)
    {
        try
        {
            // Format the start date to a string in "yyyy-MM-dd" format
            var formattedDate = startDate?.ToString("yyyy-MM-dd");

            // Log the request URL for debugging purposes
            _logger.LogInformation("Fetching weather forecast for start date: {StartDate}", formattedDate);

            // Make the HTTP GET request to fetch the weather forecast data
            var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>($"WeatherForecast?startDate={formattedDate}");

            // Return the response or an empty array if the response is null
            return response ?? Array.Empty<WeatherForecast>();
        }
        catch (HttpRequestException httpEx)
        {
            // Log HTTP request specific errors
            _logger.LogError(httpEx, "HTTP request error while fetching weather forecast");
            return Array.Empty<WeatherForecast>();
        }
        catch (Exception ex)
        {
            // Log any other exceptions
            _logger.LogError(ex, "Error fetching weather forecast");
            return Array.Empty<WeatherForecast>();
        }
    }
}
