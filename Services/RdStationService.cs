using RdStationFunction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RdStationFunction.Services
{
    public class RdStationService
    {
        private static string GetAccessToken(ClientCredential credential)
        {
            return HttpService.GetToken(credential).AccessToken;
        }

        public static async Task<bool> UpdateTags(string email, string tag, ClientCredential credential)
        {
            var accessToken = GetAccessToken(credential);

            var contact = HttpService.GetContact(email, accessToken);

            contact.AddTag(tag);

            var updateContact = new ContactUpdateTag(contact.Email, contact.Tags);

            return await HttpService.UpdateContactTag(updateContact, contact.Uuid, accessToken);
        }
    }
}
