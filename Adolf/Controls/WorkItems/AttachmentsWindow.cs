using System.Linq;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems 
{
    /// <summary>TODO: Doesn't work- how do we query attachments on a work item!?</summary>
    public sealed class AttachmentsWindow : Window
    {
        public AttachmentsWindow(WorkItem workItem) : base("[#" + workItem.Id + "] " + workItem.Title() + " - " + "Attachments")
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
                ColorScheme = Program.ColorScheme
            };

            var items = workItem.Links.Links.Keys.ToList();
            list.SetSource(items);
            
            Add(list);
        }
    }
}