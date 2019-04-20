using System.Linq;
using System.Threading.Tasks;
using Adolf.Attributes;
using Adolf.Controls.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
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
            
            // Get the specified work item
            // Why do i need to use Task.Run :(
            var workItem = Task.Run(async () => await witClient.GetWorkItemAsync(WorkItemId)).Result;
            var comments = Task.Run(async () => await witClient.GetCommentsAsync(WorkItemId)).Result;
            
            Application.Top.Add(WorkItemMenu.For(workItem).ToArray());
            
            var windows = new View[]
            {
                new DescriptionWindow(workItem),
                new AcceptanceCriteriaWindow(workItem),                
                new QuestionsClarificationsWindow(workItem),                
                new CommentsWindow(workItem, comments), 
                new FieldsWindow(workItem),
            };
            
            Application.Top.Add(windows);            
            return Task.CompletedTask;
        }
    }
}