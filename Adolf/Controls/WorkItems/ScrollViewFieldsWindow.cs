using System;
using System.Linq;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    /// <summary>
    /// Fields window implemented with a scrollview but its crap, doesn't work properly
    /// TODO: Reimplement with a ListView
    /// </summary>
    public sealed class ScrollViewFieldsWindow : Window
    {
        public ScrollViewFieldsWindow(WorkItem workItem) : base("[#" + workItem.Id + "] " + workItem.Title() + " - " + "Fields")
        {
            X = 0;
            Y = 2; // Leave one row for the toplevel menu
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = Program.ColorScheme;

            var ignore = new[] {"AudaciaScrum.QuestionsandClarifications", "System.Description", "Microsoft.VSTS.Common.AcceptanceCriteria"};
            int y = 1;

            var scroll = new ScrollView(new Rect(0, 1, Console.WindowWidth - 2, Console.WindowHeight - 6))
            {
                //Height = Dim.Percent(100),
                //Width = Dim.Percent(100),
                ShowVerticalScrollIndicator = true,
                ShowHorizontalScrollIndicator = false,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ContentSize = new Size(Console.WindowWidth - 2, (workItem.Fields.Count(x => !ignore.Contains(x.Key)) * 2) + 1),
                ContentOffset = new Point(0, 0),
                CanFocus = true
            };

            Add(scroll);
            
            foreach (var field in workItem.Fields.Where(x => !ignore.Contains(x.Key)))
            {
                var label = new Label(field.Key.Split('.').Last())
                {
                    X = 2,
                    Y = y,
                    Width = Dim.Percent(20),
                    ColorScheme = Program.ColorScheme, 
                    CanFocus = false,
                    TextAlignment = TextAlignment.Right,
                };

                scroll.Add(label);

                scroll.Add(new TextField(field.Value.ToString())
                {
                    X =  30,
                    Y = y,
                    Width = Dim.Percent(60),
                    ColorScheme = Program.ColorScheme,
                    CanFocus = false,
                });

                y++;
                y++;
            }
        }
    }
}