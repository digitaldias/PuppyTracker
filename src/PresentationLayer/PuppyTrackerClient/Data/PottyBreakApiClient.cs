using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuppyApi.Domain.Entities;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuppyTrackerClient.Data
{
    public class PottyBreakApiClient : PottyTrackerApiClientBase
    {
        public event Func<PottyBreak, Task> Notify;

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

        public async Task DeletePottyBreak(PottyBreak pottyBreak)
        {
            var url = $"{ResourceUrl}/{pottyBreak.Id.ToString()}";
            var response = await HttpClient.DeleteAsync(url);

            if(!response.IsSuccessStatusCode)
            {
                //TODO: Log this too
            }
            else
            {
                Notify?.Invoke(pottyBreak);
            }
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
            else
            {
                Notify?.Invoke(pottyBreak);
            }
        }
    }
}
