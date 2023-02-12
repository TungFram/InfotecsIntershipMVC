using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.CSV
{
    public interface ICsvService
    {
        public IEnumerable<StringRecordEntity> ReadCSV(
            Stream fileStream,
            string delimiter = ";",
            int rowCount = 10000);
    }
}
