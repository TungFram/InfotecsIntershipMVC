using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class ResultEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResultID { get; }

        [Required] public string    FileName        { get; }
        [Required] public TimeSpan  AllTime         { get; }
        [Required] public DateTime  FirstOperation  { get; }
        [Required] public int       AverageDuration { get; }
        [Required] public float     MedianByValue   { get; }
        [Required] public float     MaxValue        { get; }
        [Required] public float     AverageValue    { get; }
        [Required] public float     MinValue        { get; }
        [Required] public int       RowCount        { get; }

    }
}
