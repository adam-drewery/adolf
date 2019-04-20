using System;
using System.Collections.Generic;
using System.Diagnostics;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Controls.WorkItems
{
    public static class WorkItemMenu
    {
        public static IEnumerable<View> For(WorkItem item)
        {
            var details = new[]
            {
                new
                {
                    Label = "Fields (" + item.Fields.Count + ")",
                    Clicked = new Action(() => Application.Top.SetFocus<FieldsWindow>())
                },
                new
                {
                    Label = "Description (" + item.Description().LineCount() + ")",
                    Clicked = new Action(() => Application.Top.SetFocus<DescriptionWindow>())
                },
                new
                {
                    Label = "Acceptance Criteria (" + item.AcceptanceCriteria().LineCount() + ")",
                    Clicked = new Action(() => Application.Top.SetFocus<AcceptanceCriteriaWindow>())
                },
                new
                {
                    Label = "Q&C (" + item.QuestionsAndClarifications().LineCount() + ")",
                    Clicked = new Action(() => Application.Top.SetFocus<QuestionsClarificationsWindow>())
                },
                new
                {
                    Label = "Comments (" + item.CommentCount() + ")",
                    Clicked = new Action(() => Application.Top.SetFocus<CommentsWindow>())
                },
                new
                {
                    Label = "Web",
                    Clicked = new Action(() =>
                    {
                        var url = new Uri(Program.Url, $"{item.TeamProject()}/_workitems/edit/{item.Id}/");
                        Process.Start(url.ToString());
                    })
                },
            };

            var x = 1;
            foreach (var b in details)
            {
                var result = new Button(x, 0, b.Label) {Clicked = b.Clicked };
                x = result.Frame.Right + 1;
                yield return result;
            }
        }
    }
}