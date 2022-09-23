using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


var options = new SaveDialogOptions
{
	Title = "Save an Image",
	Filters = new FileFilter[]
	   {
			new FileFilter { Name = "Images", Extensions = new string[] {"jpg", "png", "gif" } }
	   }
};

var menu = new MenuItem[] {
	new MenuItem { Label = "Edit", Type = MenuType.submenu, Submenu = new MenuItem[] {
		new MenuItem { Label = "Undo", Accelerator = "CmdOrCtrl+Z", Role = MenuRole.undo },
		new MenuItem { Label = "Redo", Accelerator = "Shift+CmdOrCtrl+Z", Role = MenuRole.redo },
		new MenuItem { Type = MenuType.separator },
		new MenuItem { Label = "Cut", Accelerator = "CmdOrCtrl+X", Role = MenuRole.cut },
		new MenuItem { Label = "Copy", Accelerator = "CmdOrCtrl+C", Role = MenuRole.copy },
		new MenuItem { Label = "Paste", Accelerator = "CmdOrCtrl+V", Role = MenuRole.paste },
		new MenuItem { Label = "Select All", Accelerator = "CmdOrCtrl+A", Role = MenuRole.selectall }
	}
	},
	new MenuItem { Label = "View", Type = MenuType.submenu, Submenu = new MenuItem[] {
		new MenuItem
		{
			Label = "Save",
			Click = async () =>
			{
				var mainWindow = Electron.WindowManager.BrowserWindows.First();
				var result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
			}
		},
		new MenuItem
		{
			Label = "Reload",
			Accelerator = "CmdOrCtrl+R",
			Click = () =>
			{
                // on reload, start fresh and close any old
                // open secondary windows
                Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow => {
					if(browserWindow.Id != 1)
					{
						browserWindow.Close();
					}
					else
					{
						browserWindow.Reload();
					}
				});
			}
		},
		new MenuItem
		{
			Label = "Toggle Full Screen",
			Accelerator = "CmdOrCtrl+F",
			Click = async () =>
			{
				bool isFullScreen = await Electron.WindowManager.BrowserWindows.First().IsFullScreenAsync();
				Electron.WindowManager.BrowserWindows.First().SetFullScreen(!isFullScreen);
			}
		},
		new MenuItem
		{
			Label = "Open Developer Tools",
			Accelerator = "CmdOrCtrl+I",
			Click = () => Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools()
		},
		new MenuItem
		{
			Type = MenuType.separator
		},
		new MenuItem
		{
			Label = "App Menu Demo",
			Click = async () => {
				var options = new MessageBoxOptions("This demo is for the Menu section, showing how to create a clickable menu item in the application menu.");
				options.Type = MessageBoxType.info;
				options.Title = "Application Menu Demo";
				await Electron.Dialog.ShowMessageBoxAsync(options);
			}
		}
	}
	},
	new MenuItem { Label = "Window", Role = MenuRole.window, Type = MenuType.submenu, Submenu = new MenuItem[] {
			new MenuItem { Label = "Minimize", Accelerator = "CmdOrCtrl+M", Role = MenuRole.minimize },
			new MenuItem { Label = "Close", Accelerator = "CmdOrCtrl+W", Role = MenuRole.close }
	}
	},
	new MenuItem { Label = "Help", Role = MenuRole.help, Type = MenuType.submenu, Submenu = new MenuItem[] {
		new MenuItem
		{
			Label = "Learn More",
			Click = async () => await Electron.Shell.OpenExternalAsync("https://github.com/ElectronNET")
		}
	}
	}
};

Electron.Menu.SetApplicationMenu(menu);

Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(option));
app.Run();


