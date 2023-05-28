using FoodBuddy.Data;
using FoodBuddy.Models;

namespace FoodBuddy.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly DatabaseContext ctx;
        public RecipeService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Recipe model)
        {
            try
            {
                ctx.Recipe.Add(model);
                ctx.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var recipeGenre = new RecipeGenre
                    {
                        RecipeId = model.Id,
                        GenreId = genreId
                    };
                    ctx.RecipeGenre.Add(recipeGenre);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = GetById(id);
                if (data == null)
                    return false;
                var recipeGenres = ctx.RecipeGenre.Where(a => a.RecipeId == data.Id);
                foreach (var recipeGenre in recipeGenres)
                {
                    ctx.RecipeGenre.Remove(recipeGenre);
                }
                ctx.Recipe.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Recipe GetById(int id)
        {
            return ctx.Recipe.Find(id);
        }

        public RecipeListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new RecipeListVm();
            var list = ctx.Recipe.ToList();
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();

            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }
            foreach (var recipe in list)
            {
                var genres = (from genre in ctx.Genre
                              join mg in ctx.RecipeGenre
                              on genre.Id equals mg.GenreId
                              where mg.RecipeId == recipe.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                recipe.GenreNames = genreNames;
            }
            data.RecipeList = list.AsQueryable();
            return data;
        }

        public bool Update(Recipe model)
        {
            try
            {
                var genresToDeleted = ctx.RecipeGenre.Where(a => a.RecipeId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach (var rGenre in genresToDeleted)
                {
                    ctx.RecipeGenre.Remove(rGenre);
                }
                foreach (int genId in model.Genres)
                {
                    var recipeGenre = ctx.RecipeGenre.FirstOrDefault(a => a.RecipeId == model.Id && a.GenreId == genId);
                    if (recipeGenre == null)
                    {
                        recipeGenre = new RecipeGenre { GenreId = genId, RecipeId = model.Id };
                        ctx.RecipeGenre.Add(recipeGenre);
                    }
                }

                ctx.Recipe.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<int> GetGenreByRecipeId(int recipeId)
        {
            var genreIds = ctx.RecipeGenre.Where(a => a.RecipeId == recipeId).Select(a => a.GenreId).ToList();
            return genreIds;
        }
    }
}
