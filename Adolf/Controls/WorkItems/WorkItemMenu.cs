using System;
using System.Collections.Generic;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    public static class WorkItemMenu
    {
        public static IEnumerable<View> For(WorkItem item)
        {
            var stuff = new[]
            {
                new
                {
                    Label = "Fields (" + item.Fields.Count + ")",
                    Action = new Action(() => Application.Top.SetFocus<FieldsWindow>())
                },
                new
                {
                    Label = "Description (" + item.Description().LineCount() + ")",
                    Action = new Action(() => Application.Top.SetFocus<DescriptionWindow>())
                },
                new
                {
                    Label = "Acceptance Criteria (" + item.AcceptanceCriteria().LineCount() + ")",
                    Action = new Action(() => Application.Top.SetFocus<AcceptanceCriteriaWindow>())
                },
                new
                {
                    Label = "Q&C (" + item.QuestionsAndClarifications().LineCount() + ")",
                    Action = new Action(() => Application.Top.SetFocus<QuestionsClarificationsWindow>())
                },
            };

            var x = 1;
            foreach (var b in stuff)
            {
                var result = new Button(x, 0, b.Label) {Clicked = b.Action};
                x = result.Frame.Right + 1;
                yield return result;
            }
        }
    }
}