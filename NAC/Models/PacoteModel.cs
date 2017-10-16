using System.ComponentModel;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace NAC.Models
{
    public class PacoteModel
    {
        [DisplayName("Id")]
        [JsonProperty("id", NullValueHandling=NullValueHandling.Ignore)]
        public string Id { get; set; }
        
        [DisplayName("Pacote")]
        [JsonProperty(PropertyName = "desc")]
        public string Desc { get; set; }
        
        [DisplayName("Valor do Pacote")]
        [JsonProperty(PropertyName = "valor")]
        public decimal Valor { get; set; }
        
        [DisplayName("Estoque")]
        [JsonProperty(PropertyName = "estoque")]
        public int Estoque { get; set; }
    }
}