//Refer
// https://stackoverflow.com/questions/61191111/create-work-item-using-azure-devops-rest-api-using-c-sharp
//https://docs.microsoft.com/en-us/rest/api/azure/devops/wit/work%20items/create?view=azure-devops-rest-5.1

using Newtonsoft.Json; //dotnet add package Newtonsoft.Json --version 12.0.3
using System;
using System.Collections.Generic;
using System.Linq; //dotnet add package System.Linq --version 4.3.0
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks; //dotnet add package System.Threading.Tasks --version 4.3.0

namespace AddWorkItemToProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string PAT = "ssmcdx3mmkbk5ypqv4ei2nz2fimryq2qko2xjr34nz4vmybw4afa"; 
            string requestUrl = "https://dev.azure.com/TopTeamIntegration/Authentication/_apis/wit/workitems/$Task?api-version=5.0";
            try
            {
                //creating objects to hold json 
                List<Object> flds = new List<Object>
                {
                    new { op = "add", path = "/fields/System.Title", value = "NewWorkItem" }
                };

                //serializing json: string 
                string json = JsonConvert.SerializeObject(flds);

                HttpClientHandler _httpclienthndlr = new HttpClientHandler();

                using (HttpClient client = new HttpClient(_httpclienthndlr))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", "", PAT))));

                    //using http PATCH method 
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json-patch+json")
                    };

                    HttpResponseMessage responseMessage = client.SendAsync(request).Result;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());                
            }
        }
    }
}