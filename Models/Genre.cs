using System.ComponentModel.DataAnnotations;

namespace FoodBuddy.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string? GenreName { get; set; }
    }
}
