using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NAC.Intermediates;
using NAC.Models;
using NAC.Serializer;
using NAC.Service;
using Newtonsoft.Json;

namespace NAC.Controllers
{
    public class AdquirenteController : Controller
    {
        private Adquirente _request = new Adquirente();
        private Request _client = new Request();
        private Pacote _pacote = new Pacote();
        private Serializers _serializer = new Serializers();
        
        public ActionResult Index()
        {
            if (Session["Token"] == null)
            {
                TempData["Status"] = "Acesso Negado. Por favor execute o login";
                TempData["Classe"] = "alert alert-danger";
                return Redirect("/Home/");
            }
            
            List<String> Indisponiveis = _request.IndisponiveisPorUsuario();

            var Pacotes = _pacote.RequestPacotes();
                        
            foreach (var Pac in Pacotes)
            {
                if (Pac.Estoque <= 0)
                {
                    Indisponiveis.Add(Pac.Desc);
                }
            }
            Session["PacotesIndisponiveis"] = Indisponiveis;
            
            var Adquirentes = "";
            List<AdquirenteModel> myDeserializedObjList = new List<AdquirenteModel>();
            
            string Filtrador = Request.QueryString["Filtrador"];
            string Pesquisa = Request.QueryString["Pesquisa"];
            string NomedoCampo = Request.QueryString["NomedoCampo"];
            
            if (Pesquisa.GetType() != typeof(String))
            {
                Pesquisa = Convert.ToString(Pesquisa);
            }


            if (!String.IsNullOrEmpty(Filtrador) && !String.IsNullOrEmpty(Pesquisa) &&
                !String.IsNullOrEmpty(NomedoCampo))
            {
                if (Pesquisa.GetType() != typeof(String))
                {
                    Pesquisa = Convert.ToString(Pesquisa);
                }

                try
                {
                    Adquirentes =
                        _request.GetAdquirentesSearch((string) Session["Usuario"], Filtrador, NomedoCampo, Pesquisa);
                    myDeserializedObjList = _serializer.convertList<AdquirenteModel>(Adquirentes);
                }

                catch (Exception)
                {
                    // log

                }
            }

            else
            {
                myDeserializedObjList = _request.BuscaUsuarioAdquirentes((string) Session["Usuario"]);
            }

            TempData["Total"] = _request.GetTotalPorUsuarioToTempData((string) Session["Usuario"]);

            return View(myDeserializedObjList);
        }


        [HttpPost]
        public ActionResult Cadastrar(AdquirenteModel Adquirente)
        {
            if (ModelState.IsValid)
            {
                int StatusCode = _request.SubmitAdquirentePost(Adquirente);

                if (StatusCode == 201)
                {
                    TempData["Status"] = "Cadastrado com sucesso";
                    TempData["Classe"] = "alert alert-success";
                }
                else
                {
                    TempData["Status"] = "Erro ao cadastrar";
                    TempData["Classe"] = "alert alert-danger";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        [HttpGet]
        public ActionResult Cadastrar(string id)
        {
            ViewBag.ListPacote = _request.ComboBoxPacotes();
            return View();
        }
        
        [HttpPost]
        public ActionResult Editar(AdquirenteModel Adquirente)
        {
            if (ModelState.IsValid)
            {
                int StatusCode = _request.SubmitAdquirentePut(Adquirente);

                if (StatusCode == 200)
                {
                    TempData["Status"] = "Editado com sucesso";
                    TempData["Classe"] = "alert alert-success";
                }
                else
                {
                    TempData["Status"] = "Erro ao editar";
                    TempData["Classe"] = "alert alert-danger";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
        
        [HttpGet]
        public ActionResult Editar(string id)
        {
            string pacote = _client.Get("/pacote/adquirente/edit/" + id + "/");
            AdquirenteModel Pacote =_serializer.convertObject<AdquirenteModel>(pacote);
            
            return View(Pacote);
        }

        public ActionResult Deletar(string id)
        {
            int StatusCode =_client.Delete("/pacote/adquirente/" + id);

            if (StatusCode == 204)
            {
                TempData["Status"] = "Deletado com sucesso";
                TempData["Classe"] = "alert alert-success";
            }
            else
            {
                TempData["Status"] = "Erro ao deletar";
                TempData["Classe"] = "alert alert-danger";
            }
            return RedirectToAction("Index");
        }

    }
}