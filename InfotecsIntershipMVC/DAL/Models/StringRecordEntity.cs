using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class StringRecordEntity
    {
        [Required] public string? DateTime  { get; set; }
        [Required] public string? Duraion   { get; set; }
        [Required] public string? Value     { get; set; }
    }
}
