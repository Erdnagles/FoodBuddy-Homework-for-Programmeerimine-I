using FoodBuddy.Data;
using FoodBuddy.Models;

namespace FoodBuddy.Services
{
    public interface IGenreService
    {
        bool Add(Genre model);
        bool Update(Genre model);
        Genre GetById(int id);
        bool Delete(int id);
        IQueryable<Genre> List();
    }
}
