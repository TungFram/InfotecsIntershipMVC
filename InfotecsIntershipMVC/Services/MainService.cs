using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Converting;
using InfotecsIntershipMVC.Services.CSV;
using InfotecsIntershipMVC.Services.Calculaing;
using InfotecsIntershipMVC.DAL.Repositories;

namespace InfotecsIntershipMVC.Services
{
    public class MainService
    {
        private FilesRepository _filesRepository;

        private readonly ICsvService _csvService;
        private readonly IConvertingService _convertingService;
        /*private readonly ICalculatingValuesService _calculaingValuesServise;*/

        public MainService(
            FilesRepository filesRepository,
            ICsvService csvService, 
            IConvertingService convertingService/*,
            ICalculatingValuesService calculaingValuesServise*/)
        {
            _filesRepository = filesRepository;
            _csvService = csvService;
            _convertingService = convertingService;
            /*_calculaingValuesServise = calculaingValuesServise;*/
        }

        internal void SendAndReadCsv(IFormFile file)
        {
            if (file.Length == 0)
                return; // File is empty. No exception because of expensive.

            IEnumerable<StringRecordEntity> fileData = _csvService.ReadCSV(file.OpenReadStream());

            //прочитать хедеры файла и передать в метод, там проверить есть ли они,
            //если есть, то прочитать их и замапить свойства

            FileEntity fileEntityBeforePersistingInDB =
                _convertingService.ConvertFileData(fileData, file.FileName);

            _filesRepository.Create(fileEntityBeforePersistingInDB);
            
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
    