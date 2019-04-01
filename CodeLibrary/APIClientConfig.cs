using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CodeLibrary
{
    public static class APIClientConfig
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:60168/api/")
                //BaseAddress = new Uri("http://localhost:40168/api/")
            };

            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
