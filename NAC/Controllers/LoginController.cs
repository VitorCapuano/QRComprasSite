using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using NAC.Intermediates;
using NAC.Models;
using NAC.Serializer;
using NAC.Service;
using RestSharp;

namespace NAC.Controllers
{
    public class LoginController : Controller
    {
        Login _login = new Login();
        private Serializers _serializer = new Serializers();
        private Pacote _pacote = new Pacote();
        private Request _client = new Request();
        
        public ActionResult Sair()
        {
            int StatusCode = _login.UserLogout();

            if (StatusCode == 201)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Status"] = "Ocorreu um erro ao sair. Por favor tente de novo!";
                TempData["Classe"] = "alert alert-danger";
                return RedirectToAction("Index");
            }
        }
        // GET
        public ActionResult Index()
        {
            // Session.Abandon();
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(UserModel Usuario)
        {
            IRestResponse Response = _login.SubmitUserPost(Usuario);
            
            if ((int)Response.StatusCode == 200)
            {
                TokenModel Token =_serializer.convertObject<TokenModel>(Response.Content);
                
                IRestResponse ResponseUser =_login.GetUser(Token.key);
                UserModel UserModel = _serializer.convertObject<UserModel>(ResponseUser.Content);
                
                TempData["Status"] = UserModel.username+ " logado com sucesso";

                Session["Usuario"] = UserModel.username;
                
                TempData["Classe"] = "alert alert-success";
                Session["Token"] = Token.key;
                
                return Redirect("/Adquirente/");
            }
            else
            {
                TempData["Status"] = "Usuário Negado";
                TempData["Classe"] = "alert alert-danger";
                
                return Redirect("/Login/");
            }
            
        }
        
    }
}