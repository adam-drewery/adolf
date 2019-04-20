using System;
using System.Linq;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    public sealed class CommentsWindow : Window
    {
        public CommentsWindow(WorkItem workItem, WorkItemComments comments) : base("[#" + workItem.Id + "] " + workItem.CommentCount() + " - " + "Comments")
        {
            X = 0;
            Y = 2; // Leave one row for the toplevel menu
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = Program.ColorScheme;

            var list = new ListView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = Program.ColorScheme,
                CanFocus = true
            };

            var items = comments.Comments
                .SelectMany(c =>
                    new[]
                    {
                        string.Empty,
                        c.RevisedBy.Name +  " " + c.RevisedDate.ToLongTimeString(),
                        Html.Clean(c.Text)
                    }
                )
                .ToList();

            list.SetSource(items);

            Add(list);
        }
    }
}