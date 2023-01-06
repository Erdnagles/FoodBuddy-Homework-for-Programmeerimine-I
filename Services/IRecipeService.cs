using FoodBuddy.Data;
using FoodBuddy.Models;

namespace FoodBuddy.Services
{
    public interface IRecipeService
    {
        bool Add(Recipe model);
        bool Update(Recipe model);
        Recipe GetById(int id);
        bool Delete(int id);
        RecipeListVm List(string term = "", bool paging = false, int currentPage = 0);

        List<int> GetGenreByRecipeId(int recipeId);
    }
}
