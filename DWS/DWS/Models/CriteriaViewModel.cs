using System.ComponentModel.DataAnnotations;

namespace DWS.Models
{
    public class CriteriaViewModel
    {
        [Required]
        public string Criteria { get; set; }
        public string Image { get; set; }
    }
}