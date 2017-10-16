using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NAC.Models
{
    public class TotalModel
    {
        [JsonProperty("total")]
        public string Total { get; set; }
    }
}