using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Adolf.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using NStack;
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
                    Label = "Links (" + item.Attachments().Count() + ")",
                    Clicked = new Action(() => Application.Top.SetFocus<AttachmentsWindow>())
                },
                new
                {
                    Label = "Web",
                    Clicked = new Action(() =>
                    {
                        var url = new Uri(Program.Settings.Url, $"{item.TeamProject()}/_workitems/edit/{item.Id}/");
                        Process.Start(url.ToString());
                    })
                },
            };

            var x = 1;
            foreach (var b in details)
            {
                var result = new Button(x, 0, b.Label) {Clicked = b.Clicked};
                x = result.Frame.Right + 1;
                yield return result;
            }

            // hack
            yield return new Button(-1, -1, "X")
            {
                Clicked = async () =>
                {
                    Application.Top.SetFocus<AttachmentsWindow>();
                    
                    var attachmentsWindow = Application.Top.View<AttachmentsWindow>();
                    var selectedIndex = attachmentsWindow.View<ListView>().SelectedItem;
                    if (selectedIndex == 0) return;
                    
                    var attachment = item.Attachments().ElementAt(selectedIndex - 1);
                    var id = attachment.Url.Split('/').Last();
                    var stream = await Program.Api.GetClient<WorkItemTrackingHttpClient>().GetAttachmentContentAsync(new Guid(id));

                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        bytes = ms.ToArray();
                    }
                    
                    var path = Path.Combine(Path.GetTempPath(), attachment.Attributes["name"].ToString());
                    File.WriteAllBytes(path, bytes);
                    Process.Start(path);
                }
            };
        }
    }
}