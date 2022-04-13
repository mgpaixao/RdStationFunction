using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RdStationFunction.Models
{
    public class Contact
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("job_title")]
        public string JobTitle { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("website")]
        public string WebSite { get; set; }
        [JsonProperty("linkedin")]
        public string Linkedin { get; set; }
        [JsonProperty("personal_phone")]
        public string PersonalPhone { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
        [JsonProperty("extra_emails")]
        public string[] ExtraEmails { get; set; }
        [JsonProperty("cf_custom_field_2")]
        public string CfCustomField2 { get; set; }
        [JsonProperty("legal_bases")]
        public LegalBase[] LegalBases { get; set; }

        public void AddTag(string tag)
        {
            Tags = Tags.Append(tag).ToArray();
        }
    }

    public class ContactUpdateTag
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        public ContactUpdateTag(string email, string[] tags)
        {
            Email = email;
            Tags = tags;
        }
    }

    public class LegalBase
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
