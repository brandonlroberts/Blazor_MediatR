namespace Blazor_MediatR2.Components.FetchWeatherForecastData.CreateWeatherForecastComponent
{
    public record CreateWeatherForecastViewModel
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public string? CreateBy { get; set; }
    }
}
