using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
    public class NewsModel
    {
        [Required, MinLength(10, ErrorMessage = "Required to enter news name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date create is mandatory.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Required to enter description.")]
        public string Description { get; set; }
    }
}
