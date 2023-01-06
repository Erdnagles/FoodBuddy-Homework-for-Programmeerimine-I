using FoodBuddy.Models;

namespace FoodBuddy.Data
{
    public class RecipeListVm
    {
        public IQueryable<Recipe> RecipeList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
