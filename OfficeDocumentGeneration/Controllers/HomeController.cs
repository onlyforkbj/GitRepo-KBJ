using System.Web.Mvc;
using OfficeDocumentGeneration.Models;

namespace OfficeDocumentGeneration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WordDocumentModel document)
        {
            WordDocumentGenerator.GenerateOfficeDocument(document);
            return View();
        }

    }
}