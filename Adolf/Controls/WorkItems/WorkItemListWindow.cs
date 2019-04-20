using System.Collections.Generic;
using System.Linq;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    public sealed class WorkItemListWindow : Window
    {
        public WorkItemListWindow(QueryHierarchyItem query, IList<WorkItem> workItems) : base("[" + query.Name + "] ")
        {
            X = 0;
            Y = 2; // Leave one row for the toplevel menu
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = Program.ColorScheme;

            var scroll = new ListView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                CanFocus = true
            };

            Add(scroll);

            var fields = workItems
                .Where(w => w.State() != "Closed")
                .Select(i => new {Key = "[#" + i.Id + "] " + i.Title(), Value = i.State()})
                .ToList();

            var labelWidth = fields.Select(f => f.Value.Length).Max() + 2;
            var lines = fields.Select(f =>  f.Value.PadLeft(labelWidth) + " | " +  f.Key);

            scroll.SetSource(new[] {string.Empty}.Concat(lines).ToList());
        }
    }
}