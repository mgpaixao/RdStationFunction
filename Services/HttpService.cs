using Newtonsoft.Json;
using RdStationFunction.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RdStationFunction.Services
{
    public class HttpService
    {
        private static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        public static Token GetToken(ClientCredential credential)
        {
            var httpClient = GetHttpClient();

            var response =
                httpClient.PostAsJsonAsync("https://api.rd.services/auth/token", credential).Result;

            if (!response.IsSuccessStatusCode)
            {
                //Logar erro
            }

            return JsonConvert.DeserializeObject<Token>(response.Content.ReadAsStringAsync().Result);
        }

        public static Contact GetContact(string email, string accessToken)
        {
            var httpClient = GetHttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            const string urlGetContact = "https://api.rd.services/platform/contacts/email:";

            var response = httpClient.GetAsync(urlGetContact + email).Result;

            if (!response.IsSuccessStatusCode)
            {
                //Logar erro
            }

            return JsonConvert.DeserializeObject<Contact>(response.Content.ReadAsStringAsync().Result);
        }


        public static async Task<bool> UpdateContactTag(ContactUpdateTag contact, string id, string accessToken)
        {
            var bearer = "Bearer " + accessToken;

            var httpClient = RestService.For<IRdStation>("https://api.rd.services", new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });

            try
            {
                var res = await httpClient.PatchContact(id, contact, bearer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }


    interface IRdStation
    {
        [Headers("Content-Type: application/json")]
        [Patch("/platform/contacts/{id}")]
        Task<string> PatchContact([AliasAs("id")] string uuid, ContactUpdateTag contact, [Header("Authorization")] string authorization);
    }
}
