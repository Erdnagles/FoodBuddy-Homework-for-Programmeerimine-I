using System.ComponentModel.DataAnnotations;

namespace FoodBuddy.Models
{
    public class RecipeGenre
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int GenreId { get; set; }
    }
}
