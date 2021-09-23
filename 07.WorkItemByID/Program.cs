using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WorkItemByID
{
    class Program{
        static async Task Main(string[] args){
            bool temp = await GetProjects();  
            Console.WriteLine(temp);         
        }
        public static async Task<bool> GetProjects(){
            try{  
                var personalaccesstoken = "ssmcdx3mmkbk5ypqv4ei2nz2fimryq2qko2xjr34nz4vmybw4afa";

                using (HttpClient client = new HttpClient()){
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = await client.GetAsync("https://dev.azure.com/TopTeamIntegration/Authentication/_apis/wit/workitems/1?api-version=5.1"))
                    {
                        //exception handling
                        response.EnsureSuccessStatusCode();
                        // holding response as string
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
                 //returning true symbolize: the command is working.
                return true;
            }
            catch (Exception ex)
            {
                //exception handling
                Console.WriteLine(ex.ToString());
                //returning false symbolize: the command is not working
                return false;
            }
        }   

    }
}
