using System;
using System.Threading.Tasks;
using Adolf.Commands;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace Adolf
{
    public static class Program
    {
        public static VssConnection Api { get; private set; }
        
        public static ColorScheme ColorScheme { get; private set; }

        public static async Task Main(string[] args)
        {
            Application.Init();
            
            ColorScheme = new ColorScheme
            {
                Normal = Attribute.Make(Color.White, Color.Black),
                HotNormal = Attribute.Make(Color.Red, Color.Black),
                Focus = Attribute.Make(Color.Black, Color.BrightRed),
                HotFocus = Attribute.Make(Color.Red, Color.White)
            }; 
            
            Application.Top.ColorScheme = ColorScheme;
            
            var command = Command.Load(args);

            if (command is AuthCommand)
            { // Special case for AuthCommand, since it doesn't rely on a valid settings file to work.
                await command.Execute();
                return;
            } 
            
            var settings = Settings.Load();
            
            Api = new VssConnection(new Uri(settings.Url), new VssBasicCredential(string.Empty, settings.Token));
            await command.Execute();
            
            Application.Run();
            
            // Creates a menubar, the item "New" has a help menu.
            // var menu = new MenuBar(new  []
            // {
            //     new MenuBarItem("_File", new  []
            //     {
            //         new MenuItem("_New", "Creates new file", () => { }),
            //         new MenuItem("_Close", "", () =>  { }),
            //         new MenuItem("_Quit", "", () => {  })
            //     }),
            //     new MenuBarItem("_Edit", new  []
            //     {
            //         new MenuItem("_Copy", "", null),
            //         new MenuItem("_Cut", "", null),
            //         new MenuItem("_Paste", "", null)
            //     })
            // });
            // top.Add(menu);
            //
            // var login = new Label("Login: ") {X = 3, Y = 2};
            // var password = new Label("Password: ")
            // {
            //     X = Pos.Left(login),
            //     Y = Pos.Top(login) + 1
            // };
            // var loginText = new TextField("")
            // {
            //     X = Pos.Right(password),
            //     Y = Pos.Top(login),
            //     Width = 40
            // };
            // var passText = new TextField("")
            // {
            //     Secret = true,
            //     X = Pos.Left(loginText),
            //     Y = Pos.Top(password),
            //     Width = Dim.Width(loginText)
            // };
            //
            // // Add some controls, 
            // win.Add(
            //     // The ones with my favorite layout system
            //     login, password, loginText, passText,
            //
            //     // The ones laid out like an australopithecus, with absolute positions:
            //     new CheckBox(3, 6, "Remember me"),
            //     new RadioGroup(3, 8, new[] {"_Personal", "_Company"}),
            //     new Button(3, 14, "Ok"),
            //     new Button(10, 14, "Cancel"),
            //     new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar"));
        }
    }
}