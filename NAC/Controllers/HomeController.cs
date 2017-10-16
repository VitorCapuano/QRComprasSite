using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using NAC.Intermediates;
using NAC.Models;
using NAC.Serializer;
using NAC.Service;

namespace NAC.Controllers
{
    public class HomeController : Controller
    {
       private Pacote _request = new Pacote();
       private Request _client = new Request();
        
        public ActionResult Index()
        {
            var Pacotes = _request.RequestPacotes();
            
            return View(Pacotes);
        }
    }
}