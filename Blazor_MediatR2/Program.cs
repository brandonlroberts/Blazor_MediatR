using Blazor_MediatR2;
using Blazor_MediatR2.Components;
using Blazor_MediatR2.Components.FetchWeatherForecastData.CreateWeatherForecastComponent;
using Blazored.LocalStorage;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateWeatherForecastRequestHandler).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(CreateWeatherForecastRequestHandler).Assembly);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

builder.Services.AddBlazoredLocalStorageAsSingleton(); 
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IEventAggregatorService, EventAggregatorService>();

await builder.Build().RunAsync();
