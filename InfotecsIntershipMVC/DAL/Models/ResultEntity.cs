using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class ResultEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResultID { get; }

        [Required] public string    FileName        { get; set; }
        [Required] public TimeSpan  AllTime         { get; set; }
        [Required] public DateTime  FirstOperation  { get; set; }
        [Required] public int       AverageDuration { get; set; }
        [Required] public float     MedianByValue   { get; set; }
        [Required] public float     MaxValue        { get; set; }
        [Required] public float     AverageValue    { get; set; }
        [Required] public float     MinValue        { get; set; }
        [Required] public int       RowCount        { get; set; }

    }
}
