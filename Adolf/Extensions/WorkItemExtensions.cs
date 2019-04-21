using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace Adolf.Extensions
{
    public static class WorkItemExtensions
    {
        public static string AreaPath(this WorkItem workItem) => workItem.Fields["System.AreaPath"].ToString();
        
        public static string TeamProject(this WorkItem workItem) => workItem.Fields["System.TeamProject"].ToString();
        
        public static string IterationPath(this WorkItem workItem) => workItem.Fields["System.IterationPath"].ToString();
        
        public static string WorkItemType(this WorkItem workItem) => workItem.Fields["System.WorkItemType"].ToString();
        
        public static string State(this WorkItem workItem) => workItem.Fields["System.State"].ToString();
        
        public static string Reason(this WorkItem workItem) => workItem.Fields["System.Reason"].ToString();
        
        public static string AssignedTo(this WorkItem workItem) => workItem.Fields["System.AssignedTo"].ToString();
        
        public static string CreatedDate(this WorkItem workItem) => workItem.Fields["System.CreatedDate"].ToString();
        
        public static string CreatedBy(this WorkItem workItem) => workItem.Fields["System.CreatedBy"].ToString();
        
        public static string ChangedDate(this WorkItem workItem) => workItem.Fields["System.ChangedDate"].ToString();
        
        public static string ChangedBy(this WorkItem workItem) => workItem.Fields["System.ChangedBy"].ToString();
        
        public static string CommentCount(this WorkItem workItem) => workItem.Fields["System.CommentCount"].ToString();
        
        public static string Title(this WorkItem workItem) => workItem.Fields["System.Title"].ToString();
        
        public static string BoardColumn(this WorkItem workItem) => workItem.Fields["System.BoardColumn"].ToString();
        
        public static string BoardColumnDone(this WorkItem workItem) => workItem.Fields["System.BoardColumnDone"].ToString();
        
        public static string BoardLane(this WorkItem workItem) => workItem.Fields["System.BoardLane"].ToString();
        
        public static string StateChangedDate(this WorkItem workItem) => workItem.Fields["Microsoft.VSTS.Common.StateChangedDate"].ToString();
        
        public static string Priority(this WorkItem workItem) => workItem.Fields["Microsoft.VSTS.Common.Priority"].ToString();
        
        public static string ValueArea(this WorkItem workItem) => workItem.Fields["Microsoft.VSTS.Common.ValueArea"].ToString();
        
        public static string StackRank(this WorkItem workItem) => workItem.Fields["Microsoft.VSTS.Common.StackRank"].ToString();
        
        public static string AcceptanceCriteria(this WorkItem workItem) => Html.Clean(workItem.Fields["Microsoft.VSTS.Common.AcceptanceCriteria"].ToString());
        
        public static string StoryPoints(this WorkItem workItem) => workItem.Fields["Microsoft.VSTS.Scheduling.StoryPoints"].ToString();
        
        public static string Description(this WorkItem workItem) => Html.Clean(workItem.Fields["System.Description"].ToString());
        
        public static string QuestionsAndClarifications(this WorkItem workItem) => workItem.Fields["AudaciaScrum.QuestionsandClarifications"].ToString();
        
        public static string[] Tags(this WorkItem workItem) => workItem.Fields["System.Tags"].ToString()
            .Split(',')
            .Select(s => s.TrimEnd(' '))
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        public static IEnumerable<WorkItemRelation> Attachments(this WorkItem workItem) => workItem.Relations.Where(r => r.Rel == "AttachedFile");
    }
}