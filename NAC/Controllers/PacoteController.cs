using System;
using System.ComponentModel;
using System.Web.Mvc;
using NAC.Intermediates;
using NAC.Models;
using NAC.Serializer;
using NAC.Service;
using Newtonsoft.Json;
using RestSharp;

namespace NAC.Controllers
{
    public class PacoteController : Controller
    {
        private Pacote _request = new Pacote();
        private Request _client = new Request();
        private Serializers _serializer = new Serializers();
        
        public ActionResult Index()
        {
            if (Session["Token"] == null) 
            {
                TempData["Status"] = "Acesso Negado. Por favor execute o login";
                TempData["Classe"] = "alert alert-danger";
                return Redirect("/Home/");
            }
            
            var Pacotes = _request.RequestPacotes();            
            
            return View(Pacotes);
        }
        
        [HttpPost]
        public ActionResult Cadastrar(PacoteModel Pacote)
        {
            if (Session["Token"] == null) 
            {
                TempData["Status"] = "Acesso Negado. Por favor execute o login";
                TempData["Classe"] = "alert alert-danger";
                return Redirect("/Home/");
            }

            if (ModelState.IsValid)
            {
                int StatusCode = _request.SubmitPacotePost(Pacote);

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
        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Deletar(string id)
        {
            if (Session["Token"] == null) 
            {
                TempData["Status"] = "Acesso Negado. Por favor execute o login";
                TempData["Classe"] = "alert alert-danger";
                return Redirect("/Home/");
            }
            
            int StatusCode =_client.Delete("/pacote/" + id);

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
        
        [HttpGet]
        public ActionResult Editar(string id)
        {
            if (Session["Usuario"] == null) 
            {
                TempData["Status"] = "Acesso Negado. Por favor execute o login";
                TempData["Classe"] = "alert alert-danger";
                return Redirect("/Home/");
            }
            
            string pacote = _client.Get("/pacote/" + id + "/");
            PacoteModel Pacote =_serializer.convertObject<PacoteModel>(pacote);
            
            return View(Pacote);
        }
        
        [HttpPost]
        public ActionResult Editar(PacoteModel Pacote)
        {
            if (ModelState.IsValid)
            {
                int StatusCode = _request.SubmitPacotePut(Pacote);
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
    }
}