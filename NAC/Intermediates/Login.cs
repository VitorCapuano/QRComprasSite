using System;
using NAC.Models;
using Newtonsoft.Json;
using RestSharp;

namespace NAC.Intermediates
{
    public class Login
    {
        public IRestResponse SubmitUserPost(UserModel User)
        {           
            var client = new RestClient("https://nac-proxy.herokuapp.com/usuario/login/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic dml0b3I6amFtZXNib25k");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(User), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }
        
        public int UserLogout()
        {           
            var client = new RestClient("https://nac-proxy.herokuapp.com/usuario/logout/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);

            return (int)response.StatusCode;
        }
        public IRestResponse GetUser(String token)
        {           
            var client = new RestClient("https://nac-proxy.herokuapp.com/usuario/user/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-xrsf-token", token);
            request.AddHeader("authorization", "Basic dml0b3I6amFtZXNib25k");
            request.AddHeader("content-type", "application/json");
            IRestResponse Response = client.Execute(request);

            return Response;
        }
    }
}