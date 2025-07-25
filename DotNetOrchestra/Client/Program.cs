using DotNetOrchestra.Client;
using DotNetOrchestra.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri($"http://localhost:{builder.Configuration["ApiPort"]}") });
builder.Services.AddTransient<ApiService>();
builder.Services.AddSingleton<LoadingService>();

await builder.Build().RunAsync();
