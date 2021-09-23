using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GetWorkItemLinkType
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

                    using (HttpResponseMessage response = await client.GetAsync("https://dev.azure.com/TopTeamIntegration/_apis/wit/workitemrelationtypes?&api-version=5.1"))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }   

    }
}
