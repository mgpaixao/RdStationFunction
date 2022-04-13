using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace RdStationFunction.Models
{
    public class ClientCredential
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
