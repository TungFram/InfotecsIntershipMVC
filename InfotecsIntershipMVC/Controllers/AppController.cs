using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services;
using InfotecsIntershipMVC.Services.Filtering.Filters;
using InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters.AverageDuration;
using InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters.AverageValue;
using InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters.FirstOperation;
using InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.StringFilters;
using InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.Filename;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsIntershipMVC.Controllers
{
    [Route("[controller]/[action]")]
    public class AppController : MyBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MainService _mainService;

        public AppController(ILogger<HomeController> logger, MainService mainService)
        {
            _logger = logger;
            _mainService = mainService;
        }


        [HttpGet] public IActionResult Welcome() { return View(); }

        [HttpGet]
        public IActionResult UploadCsvFile()
        {
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public IActionResult UploadCsvFile(IFormFile csvFile)
        {
            FileEntity file = _mainService.SendAndReadCsv(csvFile);

            return RedirectToAction(nameof(GetResults));
        }


        [HttpGet]
        public IActionResult GetResults()
        {
            return View(_mainService.GetResults());
        }

        /*[HttpPost]
        public IActionResult GetResults(IEnumerable<AcFilter> filters)
        {
            IEnumerable<ResultEntity> filtered = _mainService.ApplyFiltersToData(filters);


            return Ok($""); 
        }*/

        [HttpGet]
        public IActionResult FilterResults()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetResults([FromForm] ResultsFilterModel filterModel)
        {
            if (filterModel == null) { return View(filterModel); }
            var filters = new List<AcFilter<ResultEntity>>();

            if (filterModel.IsFilenameAscSort)
                filters.Add(new ResultFilenameAscFilter());

            if (filterModel.IsFilenameDescSort)
                filters.Add(new ResultFilenameDescFilter());

            if (filterModel.FirstOperaionStart != filterModel.FirstOperaionEnd)
                filters.Add(
                    new FirstOperationScopedFilter()
                    .WithBoundaries(filterModel.FirstOperaionStart, filterModel.FirstOperaionEnd));

            if (filterModel.AverageDuraionStart != filterModel.AverageDuraionEnd)
                filters.Add(
                    new AvgDurationScopedFilter()
                    .WithBoundaries(filterModel.AverageDuraionStart, filterModel.AverageDuraionEnd));

            if (filterModel.AverageValueStart != filterModel.AverageValueEnd)
                filters.Add(
                    new AvgValueScopedFilter()
                    .WithBoundaries(filterModel.AverageValueStart, filterModel.AverageValueEnd));


            IEnumerable<ResultEntity> filtered = _mainService.ApplyResultFilters(filters);


            return View(filtered);
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            return View(_mainService.GetRecords());
        }

        [HttpGet]
        public IActionResult FilterRecords()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FilterRecords([FromForm] RecordsFilterModel filterModel)
        {
            if (filterModel == null)
                return View(filterModel);

            var filters = new List<AcFilter<RecordEntity>>();


            if (filterModel.FileName != default)
            {
                filters.Add(
                    new RecordFilenameEqualsPatternFilter()
                    .WithPattern(filterModel.FileName));
            }
            else
            {
                if (filterModel.ContainsPattern != default)
                    filters.Add(
                        new RecordFileNameContainsPatternFilter()
                        .WithPattern(filterModel.ContainsPattern));

                if(filterModel.StartWithPattern != default)
                    filters.Add(
                        new RecordFilenameStartsWithPatternFilter()
                        .WithPattern(filterModel.StartWithPattern));

                if (filterModel.IsFilenameAscSort != default)
                    filters.Add(new RecordFilenameAscFilter());

                if (filterModel.IsFilenameDescSort != default)
                    filters.Add(new RecordFilenameDescFilter());

            }

            IEnumerable<RecordEntity> filtered = _mainService.ApplyRecordsFilters(filters);


            return View(filtered);

        }

    }

}


