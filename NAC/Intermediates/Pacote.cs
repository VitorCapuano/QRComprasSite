using System;
using System.Collections.Generic;
using NAC.Models;
using NAC.Serializer;
using NAC.Service;
using Newtonsoft.Json;
using RestSharp;

namespace NAC.Intermediates
{
    public class Pacote
    {
        private Request _client = new Request();
        private Serializers _serial = new Serializers();

        public IList<PacoteModel> RequestPacotes()
        {   
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "e1acadbc-5bd9-ff16-2569-62d5393fd5f6");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n\t\"email\":\"\"\n\t\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return _serial.convertList<PacoteModel>(response.Content);
        }
        
        public int SubmitPacotePost(PacoteModel Pacote)
        {           
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic Og==");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(Pacote) , ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return (int)response.StatusCode;
        }
        
        public int SubmitPacotePut(PacoteModel Pacote)
        {
            string payload = JsonConvert.SerializeObject(Pacote);
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/"+Pacote.Id+"/");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic Og==");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", payload , ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return (int)response.StatusCode;
        }
    }
}