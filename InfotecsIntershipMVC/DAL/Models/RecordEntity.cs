using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class RecordEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RecordID { get; set; }

        public Guid FileID { get; set; }

        /*[ForeignKey()]*/
        [JsonIgnore]
        public FileEntity File { get; set; }


        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int Duraion { get; set; }

        [Required]
        public float Value { get; set; }

    }
}
