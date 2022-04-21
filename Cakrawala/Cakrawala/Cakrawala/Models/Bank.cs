using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class Bank
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "account_number")]
        public int AccountNumber { get; set; }
    }
}
