using System;
using System.Collections.Generic;
using System.Net;
using NAC.Models;
using Newtonsoft.Json;
using RestSharp;

namespace NAC.Service
{
    public class Request
    {
        private string BaseURL = "https://nac-proxy.herokuapp.com/";
        public string Get(
            string resource,
            Dictionary<string, string> Headers = null,
            Dictionary<string, string> QueryString = null
        )
        {

            var client = new RestSharp.RestClient(BaseURL);
            try
            {
                var request = new RestRequest(resource, Method.GET);
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                return content;
            }
            catch (Exception)
            {
                return "Erro na requisição";
            }

        }

        public int Post(
           string Resource,
           Object Payload,
           Dictionary<string, string> Headers = null,
           Dictionary<string, string> QueryString = null
       )
        {

            var Client = new RestClient(BaseURL);

            var Request = new RestRequest(Resource, Method.POST);
            
            var result = JsonConvert.SerializeObject(Payload);
            
            Request.AddHeader("content-type", "application/json");
            Request.AddParameter("application/json", result , ParameterType.RequestBody);

            
            return 201;
        }

        public int Put(string Resource, Object objeto)
        {
            int resposta = 0;

            var Client = new RestClient(BaseURL);

            var Request = new RestRequest(Resource, Method.PUT);
            Request.AddJsonBody(objeto);
            Request.RequestFormat = DataFormat.Json;
            return DoRequestAndReturnStatusCode(Client, Request);
        }

        public int Delete(string Resource)
        {
            int resposta = 0;

            var client = new RestClient(BaseURL);
            var request = new RestRequest(Resource, Method.DELETE);
            return DoRequestAndReturnStatusCode(client, request);
        }

        private int DoRequestAndReturnStatusCode(
            RestClient Client,
            RestRequest Request)
        {

            var resposta = 0;

            try
            {
                resposta = (int)Client.Execute(Request).StatusCode;
            }
            catch (Exception error)
            {
                resposta = 500;
            }
            return resposta;
        }
    }
}