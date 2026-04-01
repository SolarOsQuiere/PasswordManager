using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PasswordManager;
using PasswordManager.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<TextVisibilityService>();
builder.Services.AddScoped<AppRoutesService>();
builder.Services.AddScoped<DeterministicSecretService>();

await builder.Build().RunAsync();
