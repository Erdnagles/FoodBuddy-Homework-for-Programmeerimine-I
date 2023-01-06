using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodBuddy.Services;
using FoodBuddy.Models;

namespace FoodBuddy.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IFileService _fileservice;
        private readonly IGenreService _genService;
        public RecipeController(IGenreService genService, IRecipeService RecipeService, IFileService fileService)
        {
            _recipeService = RecipeService;
            _fileservice = fileService;
            _genService = genService;
            
        }
        public IActionResult Add()
        {
            var model = new Recipe();
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Recipe model)
        {
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileResult = this._fileservice.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                }
                var imageName = fileResult.Item2;
                model.RecipeImage = imageName;
            }
            var result  = _recipeService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Recipe could not be saved (Error on server side)";
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var model = _recipeService.GetById(id);
            var selectedGenres = _recipeService.GetGenreByRecipeId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Recipe model)
        {
            var selectedGenres = _recipeService.GetGenreByRecipeId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileResult = this._fileservice.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.RecipeImage = imageName;
            }
            var result = _recipeService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(RecipeList));
            }
            else
            {
                TempData["msg"] = "Recipe could not be saved (Error on server side)";
                return View(model);
            }
        }

        public IActionResult RecipeList()
        {
            var data = this._recipeService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _recipeService.Delete(id);
            return RedirectToAction(nameof(RecipeList));
        }
    }
}
