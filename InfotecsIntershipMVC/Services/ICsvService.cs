using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services
{
    public interface ICsvService
    {
        public IEnumerable<StringRecordEntity> ReadCSV(
            Stream fileStream,
            string delimiter = ";",
            int rowCount = 10000);      
    }
}
