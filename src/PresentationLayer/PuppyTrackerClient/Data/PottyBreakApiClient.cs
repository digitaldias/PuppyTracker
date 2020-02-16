using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuppyApi.Domain.Entities;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuppyTrackerClient.Data
{
    public class PottyBreakApiClient : PottyTrackerApiClientBase
    {
        public PottyBreakApiClient()
            : base("potty")
        {
        }

        public async Task<PottyBreak[]> GetPottyBreaksAsync()
        {
            var breaks = await  HttpClient.GetJsonAsync<PottyBreak[]>(ResourceUrl);
           
            if (breaks != null && breaks.Any())
                return breaks.OrderByDescending(b => b.DateTime).ToArray();

            return null;
        }

        public async Task SaveOrUpdatePottyBreak(PottyBreak pottyBreak)
        {
            var json     = JsonConvert.SerializeObject(pottyBreak);            
            var content  = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(ResourceUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                //TODO: Let's log this
            }
        }
    }
}
