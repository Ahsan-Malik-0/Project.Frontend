using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project.Frontend;
using Project.Frontend.AdminServices;
using Project.Frontend.Auth.Services;
using Project.Frontend.Chairperson.Services;
using Project.Frontend.President.Services;
using Project.Frontend.Shared.Services;
using Project.Frontend.StudentAffairs.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7148/api/")
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ClaimServices>();
builder.Services.AddScoped<PresidentServices>();
builder.Services.AddScoped<ChairpersonServices>();
builder.Services.AddScoped<StudentAffairsServices>();
builder.Services.AddScoped<AdminServices>();
builder.Services.AddScoped<FinanceService>();

await builder.Build().RunAsync();
