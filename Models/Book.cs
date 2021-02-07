using System.ComponentModel.DataAnnotations;

namespace WookieBooks.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string CoverImage { get; set; }
        [Required]
        public double Price { get; set; }
     }
}
