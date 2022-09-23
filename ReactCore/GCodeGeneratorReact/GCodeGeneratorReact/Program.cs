using ElectronNET.API;
using ElectronNET.API.Entities;
using GCodeGeneratorReact.Interfaces;
using GCodeGeneratorReact.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISettingService, SettingService>();
builder.Services.AddTransient<GCodeService>();
builder.Services.AddControllersWithViews();
builder.WebHost.UseElectron(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
var option = new BrowserWindowOptions();
option.DarkTheme = true;
option.AutoHideMenuBar = true;
option.Height = 700;

Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(option));
app.Run();


