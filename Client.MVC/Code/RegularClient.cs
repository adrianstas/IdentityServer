using Client.MVC.Code.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.MVC.Code
{
    public static class RegularClient
    {
        public static HttpClient GetClient(string apiAddress)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}