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
    public sealed class FieldsWindow : Window
    {
        public FieldsWindow(WorkItem workItem) : base("[#" + workItem.Id + "] " + workItem.Title() + " - " + "Fields")
        {
            X = 0;
            Y = 2; // Leave one row for the toplevel menu
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = Program.ColorScheme;

            var ignore = new[] {"AudaciaScrum.QuestionsandClarifications", "System.Description", "Microsoft.VSTS.Common.AcceptanceCriteria"};
            int y = 1;

            var scroll = new ListView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                CanFocus = true
            };

            Add(scroll);

            var fields = workItem.Fields.Where(x => !ignore.Contains(x.Key))
                .Select(f => new {Key = f.Key.Split('.').Last(), f.Value})
                .ToList();

            var labelWidth = fields.Select(f => f.Key.Length).Max() + 2;
            var lines = fields.Select(f => f.Key.PadLeft(labelWidth) + " | " + f.Value);

            scroll.SetSource(new[] {string.Empty}.Concat(lines).ToList());

            // foreach (var field in workItem.Fields.Where(x => !ignore.Contains(x.Key)))
            // {
            //     var label = new Label(field.Key.Split('.').Last())
            //     {
            //         X = 2,
            //         Y = y,
            //         Width = Dim.Percent(20),
            //         ColorScheme = Program.ColorScheme, 
            //         CanFocus = false,
            //         TextAlignment = TextAlignment.Right,
            //     };
            //
            //     scroll.Add(label);
            //
            //     scroll.Add(new TextField(field.Value.ToString())
            //     {
            //         X =  30,
            //         Y = y,
            //         Width = Dim.Percent(60),
            //         ColorScheme = Program.ColorScheme,
            //         CanFocus = false,
            //     });
            //
            //     y++;
            //     y++;
            // }
        }
    }
}