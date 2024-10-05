using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPProject.Controllers
{
    public class FileUpLoad : Controller
    {
        // GET: FileUpLoad
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult FileUpload() 
        {
            return View();
        }


    }
}
