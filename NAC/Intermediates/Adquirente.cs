using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages.Html;
using NAC.Models;
using NAC.Serializer;
using NAC.Service;
using Newtonsoft.Json;
using RestSharp;

namespace NAC.Intermediates
{
    public class Adquirente
    {
        Login _login = new Login();
        private Adquirente _request = new Adquirente();
        private Request _client = new Request();
        private Pacote _pacote = new Pacote();

        private Serializers _serializer = new Serializers();
        
        public int SubmitAdquirentePost(AdquirenteModel Ad)
        {           
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/adquirente/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Basic dml0b3I6amFtZXNib25k");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(Ad), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return (int)response.StatusCode;
        }
        
        public int SubmitAdquirentePut(AdquirenteModel Ad)
        {           
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/adquirente/edit/"+Ad.Id+"/");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("authorization", "Basic dml0b3I6amFtZXNib25k");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(Ad), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return (int)response.StatusCode;
        }
        
        public string GetAdquirentes(String User)
        {      
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/adquirente/"+"?user="+User);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
        
        public string GetAdquirentesSearch(String User, String Filtrador = null,String NomeDoCampo = null, String Pesquisa = null)
        {
            String SearchA = "";
            IList<String> StringsMatch = new List<String>()
            {
                "Menor", "Menor ou Igual", "Maior", "Maior ou Igual", "Igual"
            };
            
            IList<String> StringsMatchAPI = new List<String>()
            {
                "__lt", "__lte", "__gt", "__gte", ""
            };
            
            foreach (var Var in StringsMatch.Select((valor, index) => new { index, valor }))
            {
                if (Filtrador == Var.valor)
                {
                    SearchA = StringsMatchAPI[Var.index];
                    break;
                }
            }

            SearchA = SearchA + "=" + Pesquisa;

            if (NomeDoCampo == "Pacote")
            {
                SearchA = "&pacote" + SearchA ;
            } else if (NomeDoCampo == "Quantidade")
            {
                SearchA = "&qtd" + SearchA;
            }
            else
            {
                SearchA = "&total" + SearchA;
            }
            
            var client = new RestClient("https://nac-proxy.herokuapp.com/pacote/adquirente/"+"?user="+ User +  SearchA);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
        
        public List<AdquirenteModel> BuscaFiltrada(string Usuario, string Filtrador, string Pesquisa, string NomedoCampo)
        {
            var Adquirentes = "";
            List<AdquirenteModel> myDeserializedObjList = new List<AdquirenteModel>();

            if (Pesquisa.GetType() != typeof(String))
            {
                Pesquisa = Convert.ToString(Pesquisa);
            }

            try
            {
                Adquirentes =
                    _request.GetAdquirentesSearch(Usuario, Filtrador, NomedoCampo, Pesquisa);
                myDeserializedObjList = _serializer.convertList<AdquirenteModel>(Adquirentes);
            }

            catch (Exception)
            {
                // log

            }

            return myDeserializedObjList;
        }

        public List<AdquirenteModel> BuscaUsuarioAdquirentes(string Usuario)
        {
            var Adquirentes = _request.GetAdquirentes(Usuario);
            return _serializer.convertList<AdquirenteModel>(Adquirentes);
        }

        public string GetTotalPorUsuarioToTempData(string Usuario)
        {
            string GetTotal = _client.Get("/pacote/adquirente/total/?user="+Usuario);
            return _serializer.convertObject<TotalModel>(GetTotal).Total;
        }
        
        public List<string> IndisponiveisPorUsuario()
        {
            var Pacotes = _pacote.RequestPacotes();
            List<string> Indisponiveis = new List<string>();
                        
            foreach (var Pac in Pacotes)
            {
                if (Pac.Estoque <= 0)
                {
                    Indisponiveis.Add(Pac.Desc);
                }
            }

            return Indisponiveis;
        }

        public List<PacoteModel> ComboBoxPacotes()
        {
            var DescPacotes = _serializer.convertList<PacoteModel>((_client.Get("/pacote")));

            List<SelectListItem> listItems= new List<SelectListItem>();

            foreach (var Pac in DescPacotes)
            {
                listItems.Add(new SelectListItem
                {
                    Text = "Pacote",
                    Value = Pac.Desc
                });
            }

            return DescPacotes;
        }
    }
}