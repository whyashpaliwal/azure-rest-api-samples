using System;
// using System.Net.Http;
// using System.Net.Http.Headers;
using System.Threading.Tasks;
// using Microsoft.VisualStudio.Services.Client;
// using Microsoft.TeamFoundationServer.Client;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GetWorkItemsCount
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Gettting Work items of type Task from Project Authentication");

            Uri orgUrl = new Uri("https://dev.azure.com/TopTeamIntegration");  
            String personalAccessToken = "ssmcdx3mmkbk5ypqv4ei2nz2fimryq2qko2xjr34nz4vmybw4afa";  
            VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken)); 

            // creating “WorkItemTrackingHttpClient”
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>(); 
            // creating WIQL object
            Wiql wiql = new Wiql();  
  
            // Defining WIQL query its same as SQL

            // wiql.Query = "SELECT * "  
            // + " FROM WorkItems WHERE [System.WorkItemType] in ('Bug','Product Backlog Item')"  
            // + " AND [Microsoft.VSTS.Common.ClosedDate] >'" + Convert.ToDateTime("2019-01-01").ToString() + "'"  
            // + " AND [System.CreatedDate] >'" + Convert.ToDateTime("2019-01-01").ToString() + "'"; 
            
            wiql.Query = "SELECT * "  
            + " FROM WorkItems WHERE [System.AreaPath] in ('Authentication') " 
            + " AND [System.WorkItemType] in ('Task','Product Backlog Item') "; 

            WorkItemQueryResult tasks = await witClient.QueryByWiqlAsync(wiql);  
            IEnumerable<WorkItemReference> tasksRefs;  
            tasksRefs = tasks.WorkItems.OrderBy(x => x.Id);  
            // using list to store response
            List<WorkItem> tasksList = witClient.GetWorkItemsAsync(tasksRefs.Select(wir => wir.Id)).Result;  
            
            int LWorkItemTypeCount = 0;    // counter variable
            // looping through the responses
            foreach (var task in tasksList)  
            {   
                LWorkItemTypeCount++;
                string wiTitle = task.Fields["System.Title"].ToString();
                Console.WriteLine(LWorkItemTypeCount + ". WI Title:  " + wiTitle);             
                //var LTemp = task.Id;
                //Console.WriteLine("id = " + LTemp );
            } 
            Console.WriteLine("Total WIT='Task' in Project='Authentication' are = " + LWorkItemTypeCount);
        }
    }
}
