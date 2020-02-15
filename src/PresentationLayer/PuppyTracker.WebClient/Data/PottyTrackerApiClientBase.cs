using System.Linq;
using System.Net.Http;

namespace PuppyTracker.WebClient.Data
{
    public class PottyTrackerApiClientBase
    {
        private static HttpClient _httpClient;
        private const string BASE_API_URL = "http://localhost:58102/api/";
        private readonly string _resourceUrl;

        public PottyTrackerApiClientBase(string resourceName)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();

            _resourceUrl = BASE_API_URL + resourceName;

            if (!_resourceUrl.EndsWith('/'))
                _resourceUrl.Append('/');
        }

        public HttpClient HttpClient => _httpClient;

        public string ResourceUrl => _resourceUrl;        
    }
}
