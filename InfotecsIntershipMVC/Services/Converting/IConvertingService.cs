using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Converting
{
    public interface IConvertingService
    {
        public FileEntity ConvertFileData(
            IEnumerable<StringRecordEntity> fileData,
            string fileName);
    }
}
