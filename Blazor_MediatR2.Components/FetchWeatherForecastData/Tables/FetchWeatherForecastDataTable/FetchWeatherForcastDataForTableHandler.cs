using MediatR;
using System.Net.Http.Json;
using static Blazor_MediatR2.Components.FetchWeatherForecastData.Tables.FetchWeatherForecastDataTable.FetchWeatherForcastDataForTableHandler;

namespace Blazor_MediatR2.Components.FetchWeatherForecastData.Tables.FetchWeatherForecastDataTable
{
    public class FetchWeatherForcastDataForTableHandler : IRequestHandler<GetForecast, List<FetchWeatherForecastDataViewModel>>
    {
        private readonly HttpClient _httpClient;

        public FetchWeatherForcastDataForTableHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class GetForecast : IRequest<List<FetchWeatherForecastDataViewModel>>
        {
            public int PageSize { get; set; }

            public GetForecast(int pageSize = 10)
            {
                PageSize = pageSize;
            }
        }

        public async Task<List<FetchWeatherForecastDataViewModel>> Handle(GetForecast request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<List<FetchWeatherForecastDataViewModel>>("sample-data/weather.json");
            return response?.Take(request.PageSize).ToList()!;
        }
    }
}
