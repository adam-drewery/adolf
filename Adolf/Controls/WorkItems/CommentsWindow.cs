using System;
using System.Linq;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    public sealed class CommentsWindow : Window
    {
        public CommentsWindow(WorkItem workItem, WorkItemComments comments) : base("[#" + workItem.Id + "] " + workItem.Title() + " - " + "Comments")
        {
            X = 0;
            Y = 2; // Leave one row for the toplevel menu
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = Program.ColorScheme;

            var items = comments.Comments
                .SelectMany(c => new[]
                {
                    Environment.NewLine + c.RevisedBy.Name + " " + c.RevisedDate.ToLongTimeString() +  " " + c.RevisedDate.ToLongDateString(),
                    Html.Clean(c.Text).WordWrap(Console.WindowWidth - 2),
                    string.Empty
                })
                .ToList();

            var list = new TextView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = Program.ColorScheme,
                CanFocus = true,
                Text = string.Join(Environment.NewLine, items)
            };

            Add(list);
        }
    }
}