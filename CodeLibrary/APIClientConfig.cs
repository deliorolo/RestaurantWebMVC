using System;
using System.Net.Http;
using System.Net.Http.Headers;

/* Static class that is initialize in Global.asax.cs and holds the connection
 * to the Web API service "WebApiRestaurantDataAccess" (included in same solution). 
 * The communication is done through Json and the connection is always opened while
 * application is running, being the HttpClient called when necessary. */

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
            };

            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
