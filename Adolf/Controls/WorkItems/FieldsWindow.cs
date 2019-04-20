using System;
using System.Linq;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
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
        }
    }
}