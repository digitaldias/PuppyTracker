using System.Linq;
using System.Net.Http;

namespace PuppyTrackerClient.Data
{
    public class PottyTrackerApiClientBase
    {
        private static HttpClient _httpClient;
        private const string BASE_API_URL = "http://localhost:58102/api/";

        public PottyTrackerApiClientBase(string resourceName)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();

            ResourceUrl = BASE_API_URL + resourceName;

            if (!ResourceUrl.EndsWith('/'))
                ResourceUrl.Append('/');
        }

        public HttpClient HttpClient => _httpClient;

        public string ResourceUrl { get; }
    }
}
