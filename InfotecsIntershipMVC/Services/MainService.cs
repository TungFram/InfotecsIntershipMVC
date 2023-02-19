﻿using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Converting;
using InfotecsIntershipMVC.Services.CSV;
using InfotecsIntershipMVC.Services.Calculaing;
using InfotecsIntershipMVC.DAL.Repositories;
using System.Collections.Immutable;
using System.Transactions;
using InfotecsIntershipMVC.Services.Filtering.Filters;
using InfotecsIntershipMVC.Services.Filtering;

namespace InfotecsIntershipMVC.Services
{
    public class MainService
    {
        private readonly ILogger<MainService> _logger;

        private FilesRepository _filesRepository;
        private ResultsRepository _resultsRepository;

        private readonly ICsvService _csvService;
        private readonly IConvertingService _convertingService;
        private readonly IFilteringService _filteringService;

        public MainService(
            ILogger<MainService> logger,
            FilesRepository filesRepository,
            ResultsRepository resultsRepository,
            ICsvService csvService, 
            IConvertingService convertingService,
            IFilteringService filteringService)
        {
            _logger = logger;
            _filesRepository = filesRepository;
            _resultsRepository = resultsRepository;
            _csvService = csvService;
            _convertingService = convertingService;
            _filteringService = filteringService;
        }

        internal void SendAndReadCsv(IFormFile file)
        {
            if (file.Length == 0)
                return; // File is empty. No exception because of expensive.

            IEnumerable<StringRecordEntity> fileData = _csvService.ReadCSV(file.OpenReadStream());

            //прочитать хедеры файла и передать в метод, там проверить есть ли они,
            //если есть, то прочитать их и замапить свойства

            // Get file with empty result (only name of this file)
            // and without Id.
            FileEntity fileEntity =
                _convertingService.ConvertFileData(fileData, file.FileName);

            var calculator = new CalculatingService(
                fileEntity.Records.ToImmutableList(),
                fileEntity.Result);

            fileEntity.Result = calculator.WithOperations().CalculateValues();


            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                FileEntity? existedFile = _filesRepository.FindByName(file.FileName);
                if (existedFile != null)
                    _filesRepository.DeleteById(existedFile.FileID);

                _filesRepository.Create(fileEntity);            // Set ID.

                    // The action below is no longer necessary, because
                    // the file and its result are linked via relationship,
                    // the creation will be done within EF automatically.
                /*_resultsRepository.Create(fileEntity.Result);*/   // Set ID.

                // Check for successfully transaction.
                if (_filesRepository.FindById(fileEntity.FileID) != null ||
                    _resultsRepository.FindById(fileEntity.Result.ResultID) != null)
                {
                    // Success.
                    _logger.LogInformation(
                        $"File {fileEntity.Name} was added to db " +
                        $"with {fileEntity.Result.RowCount} rows");

                    transactionScope.Complete();
                }
                else
                {
                    // Fail.
                    _logger.LogCritical(
                        $"File {fileEntity.Name} wasn't added to db!" +
                        $"Was the file before adding? {existedFile != null}.");
                }
            }
        }

        public IEnumerable<ResultEntity> ApplyFiltersToData(IEnumerable<AcFilter> filters)
        {
            IEnumerable<ResultEntity> filteredResults = _filteringService
                .WithFilters(filters)
                .WithData(_resultsRepository.GetAllImmutable())
                .ApplyFileters();

            return filteredResults;
        }
    }
}

/*Пути подсчета значений результата файла:
    I] Реализовать в репозитории методы для получения каждого значения по отдельности
        ++ Работа с контекстом, у которого есть специальные быстрые методы
        -- Принцип единственной ответственности, ведь репозиторий отвечает за создание, а не за расчет логики

    II] Брать из репозитория все записи и работать уже с ними 
        +- (+)Работа не с контекстом и запросами к бд
        , а (-)со всеми полученными записями и запросов к коллекции
        Вопрос в скорости.

        ++ Принцип единственной ответственности


        А) Создать интерфейс и классы команд, у каждой из которых единственный метод выполнения
            ++ Полиморфизм, можно в цикле команды перебрать, а в параметрах будет коллекция команд
            ++ Принцип открытости/закрытости
            ++ гибкость
            -- Много классов
            ? Возвращаемся к IvsII (где основная логика?)

                1) В основном сервисе держать
                -- Захламляет код
                -- принцип единственной ответственности

                2) Передать другому сервису

        Б) Сделать просто методы 
            1) В основном сервисе посчитать
                -- Захламляет код
                -- принцип единственной ответственности

            2) Передать другому сервису
*/
    