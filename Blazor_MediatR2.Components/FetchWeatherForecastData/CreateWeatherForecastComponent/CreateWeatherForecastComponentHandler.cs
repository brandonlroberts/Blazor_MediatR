using Blazor_MediatR2.Components.FetchWeatherForecastData.Tables.FetchWeatherForecastDataTable;
using Blazored.LocalStorage;
using FluentValidation;
using MediatR;

namespace Blazor_MediatR2.Components.FetchWeatherForecastData.CreateWeatherForecastComponent
{
    public class CreateWeatherForecastValidator : AbstractValidator<CreateWeatherForecast.Request>
    {
        public CreateWeatherForecastValidator()
        {
            RuleFor(x => x.temperatureC)
                .GreaterThan(15)
                .WithMessage("Ain't nobody wants it that cold...");

            RuleFor(x => x.summary)
                .NotEqual(string.Empty)
                .NotNull()
                .WithMessage("You gotta type something into summary");

            RuleFor(x => x.createdBy)
                .NotEqual(string.Empty)
                .NotNull()
                .WithMessage("Who is creating this???");            
        }
    }

    public record CreateWeatherForecast
    {
        public record Request(int temperatureC, string summary, string createdBy) : BlazorMediatRRequest, IRequest<Response>;

        public record Response : AuditableResponse
        {
            public CreateWeatherForecastViewModel[]? weatherForecasts;
        }
    }

    public class CreateWeatherForecastRequestHandler : IRequestHandler<CreateWeatherForecast.Request, CreateWeatherForecast.Response>
    {
        private readonly ILocalStorageService _storage;
        private readonly IEventAggregatorService _eventAggregator;
        
        public CreateWeatherForecastRequestHandler(ILocalStorageService storage, IEventAggregatorService eventAggregator)
        {
            _storage = storage;
            _eventAggregator = eventAggregator;
        }

        public async Task<CreateWeatherForecast.Response> Handle(CreateWeatherForecast.Request request, CancellationToken cancellationToken)
        {
            var response = await _storage.GetItemAsync<List<FetchWeatherForecastDataViewModel>>("forecasts");
            var forecast = new FetchWeatherForecastDataViewModel
            {
                CreateBy = request.createdBy!,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Summary = request.summary!,
                TemperatureC = request.temperatureC,
            };
            response.Add(forecast);
            await _storage.SetItemAsync("forecasts", response);
            await _eventAggregator.PublishAsync("refresh_weather_forecast_table");
            return new CreateWeatherForecast.Response();
        }
    }
}
