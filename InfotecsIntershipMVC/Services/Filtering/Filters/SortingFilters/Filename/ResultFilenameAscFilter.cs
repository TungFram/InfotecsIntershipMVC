﻿using InfotecsIntershipMVC.DAL.Models;
using System.Collections.Immutable;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.Filename
{
    public class ResultFilenameAscFilter : AcFilter<ResultEntity>
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderBy(result => result.FileName).ToList();

            return ToNextFilter();
        }
    }
}
