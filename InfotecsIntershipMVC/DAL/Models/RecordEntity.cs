using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class RecordEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RecordID { get; }

        public Guid FileID { get; }


        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int Duraion { get; set; }

        [Required]
        public float Value { get; set; }

    }
}
