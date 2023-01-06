using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodBuddy.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? ReleaseDate { get; set; }
        public string? RecipeImage { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Guide { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public string? GenreNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }
    }
}
