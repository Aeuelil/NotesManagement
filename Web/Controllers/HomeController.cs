using Microsoft.AspNetCore.Mvc;

namespace NotesManagement.Controllers
{
    
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}