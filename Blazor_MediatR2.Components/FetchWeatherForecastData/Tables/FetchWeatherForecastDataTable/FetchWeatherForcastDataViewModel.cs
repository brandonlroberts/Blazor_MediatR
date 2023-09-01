namespace Blazor_MediatR2.Components.FetchWeatherForecastData.Tables.FetchWeatherForecastDataTable
{
    public record FetchWeatherForecastDataViewModel
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? CreateBy { get; set; }

    }
}
