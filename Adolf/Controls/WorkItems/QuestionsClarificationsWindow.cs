using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    public sealed class QuestionsClarificationsWindow : Window
    {
        public QuestionsClarificationsWindow(WorkItem workItem) : base("[#" + workItem.Id + "] " + workItem.Title() + " - " + "Questions & Clarifications")
        {
            X = 0;
            Y = 2; // Leave one row for the toplevel menu
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = Program.ColorScheme;

            Add(new TextView
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = Program.ColorScheme,
                Text = workItem.QuestionsAndClarifications(),
                ReadOnly = true
            });
        }
    }
}