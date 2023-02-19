using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services;
using InfotecsIntershipMVC.Services.Filtering.Filters;
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
            _mainService.SendAndReadCsv(file);

            return Ok();
        }

        [HttpGet]
        public IActionResult FilterData([FromForm] IEnumerable<AcFilter> filters)
        {
            IEnumerable<ResultEntity> filtered = _mainService.ApplyFiltersToData(filters);


            return Ok($""); 
        }

    }

}


