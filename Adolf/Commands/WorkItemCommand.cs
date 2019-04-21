using System.Linq;
using System.Threading.Tasks;
using Adolf.Attributes;
using Adolf.Controls.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Terminal.Gui;

namespace Adolf.Commands
{
    [Alias("work")]
    public class WorkItemCommand : Command
    {
        public WorkItemCommand() { }

        public WorkItemCommand(int workItemId) => WorkItemId = workItemId;

        [DefaultArgument] public int WorkItemId { get; set; }

        public override Task Execute()
        {
            var witClient = Program.Api.GetClient<WorkItemTrackingHttpClient>();

            if (WorkItemId == 0)
            {
                QueryHierarchyItem query = null;
                var workItems = Task.Run(async () =>
                {
                    var queries = await witClient.GetQueriesAsync(Program.Settings.Project, null, 2);
                    query = await witClient.GetQueryAsync(Program.Settings.Project, "My Queries/Assigned to me", QueryExpand.Wiql);
                    var queryResult = await witClient.QueryByWiqlAsync(new Wiql {Query = query.Wiql}, Program.Settings.Project);
                    var ids = queryResult.WorkItems.Select(w => w.Id);
                    return await witClient.GetWorkItemsAsync(ids);
                }).Result;

                var window = new WorkItemListWindow(query, workItems);
                Application.Top.Add(window);
                Application.Top.SetFocus(window);
                return Task.CompletedTask;
            }

            // Get the specified work item
            // Why do i need to use Task.Run :(
            var workItem = Task.Run(async () => await witClient.GetWorkItemAsync(WorkItemId, expand: WorkItemExpand.All)).Result;
            var comments = Task.Run(async () => await witClient.GetCommentsAsync(WorkItemId)).Result;

            Application.Top.Add(WorkItemMenu.For(workItem).ToArray());

            var windows = new View[]
            {
                new DescriptionWindow(workItem),
                new AcceptanceCriteriaWindow(workItem),
                new QuestionsClarificationsWindow(workItem),
                new CommentsWindow(workItem, comments),
                new AttachmentsWindow(workItem), 
                new FieldsWindow(workItem),
            };

            Application.Top.Add(windows);
            Application.Top.SetFocus(windows.First());
            return Task.CompletedTask;
        }
    }
}