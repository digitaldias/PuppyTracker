using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuppyApi.Domain.Entities;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuppyTracker.WebClient.Data
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
            var url = ResourceUrl + pottyBreak.Id.ToString();            
            var json = JsonConvert.SerializeObject(pottyBreak);

            await base.HttpClient.PutJsonAsync(url, json);

        }
    }
}
