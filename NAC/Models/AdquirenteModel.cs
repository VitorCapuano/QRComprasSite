using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace NAC.Models
{
    public class AdquirenteModel
    {
        [DisplayName("Id")]
        [JsonProperty("id", NullValueHandling=NullValueHandling.Ignore)]
        public string Id { get; set; }
        
        [DisplayName("Usuário")]
        [JsonProperty("user")]
        public String User { get; set; }
        
        [DisplayName("Pacote")]
        [JsonProperty("pacote")]
        public String Pacote { get; set; }
        
        [DisplayName("Quantidade")]
        [JsonProperty("qtd")]
        public int Qtd { get; set; }
        
        [DisplayName("Total")]
        [JsonProperty("total")]
        public decimal Total { get; set; }
    }
    
    public class JsonAdquirente
    {
        public List<AdquirenteModel> Adquirentes { get; set; }
    }
}