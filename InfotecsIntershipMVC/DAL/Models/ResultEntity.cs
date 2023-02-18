using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class ResultEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResultID { get; set; }

        [Required] public string    FileName        { get; set; }

        // Alltime represents difference berween max datetime and min in SECONDS.
        // Max difference with seconds clould be 68 years, so it clould be used
        // within the framework of the task condition.
        // https://learn.microsoft.com/ru-ru/sql/t-sql/functions/datediff-transact-sql?view=sql-server-ver16
        [Required] public int       AllTime         { get; set; }
        [Required] public DateTime  FirstOperation  { get; set; }
        [Required] public int       AverageDuration { get; set; }
        [Required] public float     MedianByValue   { get; set; }
        [Required] public float     MaxValue        { get; set; }
        [Required] public float     AverageValue    { get; set; }
        [Required] public float     MinValue        { get; set; }
        [Required] public int       RowCount        { get; set; }

        public Guid FileID { get; set; }
        public FileEntity File { get; set; }

        public ResultEntity(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            FileName = fileName;
        }

        public ResultEntity(ResultEntity another)
        {
            ResultID = another.ResultID;
            FileName = another.FileName;
            AllTime = another.AllTime;
            FirstOperation = another.FirstOperation;
            AverageDuration = another.AverageDuration;
            MedianByValue = another.MedianByValue;
            MaxValue = another.MaxValue;
            AverageValue = another.AverageValue;
            MinValue = another.MinValue;
            RowCount = another.RowCount;      
            FileID = another.FileID;
            File = another.File;
        }

    }
}
