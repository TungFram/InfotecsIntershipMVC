using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsIntershipMVC.Controllers
{
    [Route("[controller]")]
    public class AppController : MyBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MainService _mainService;

        public AppController(ILogger<HomeController> logger, MainService mainService)
        {
            _logger = logger;
            _mainService = mainService;
        }


        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public IActionResult UploadCsvFile(IFormFile file)
        {
            var fileData = _mainService.SendAndReadCsv(file);

            return Ok(fileData);
        }

        [HttpGet]
        public IActionResult GetDate()
        {
            return Ok($"|{DateTime.Now.ToShortDateString}|{DateTime.Now.ToLongDateString}|"); 
        }

    }

}


