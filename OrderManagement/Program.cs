using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrderManagement;
using OrderManagement.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient();

/*builder.Services.AddHttpClient("OrderManagementAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7113/api/");
});
*/
await builder.Build().RunAsync();
