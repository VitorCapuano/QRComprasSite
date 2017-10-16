using Newtonsoft.Json;

namespace NAC.Models
{
    public class UserModel
    {
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }
        
        [JsonProperty(PropertyName = "password")]
        public string senha { get; set; }
    }
}