using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartRecipe.Models;
using SmartRecipe.Services;

namespace SmartRecipe.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;

    [ObservableProperty] private int _totalIngredients;
    [ObservableProperty] private int _availableIngredients;
    [ObservableProperty] private int _totalRecipes;
    [ObservableProperty] private Recipe _recipeOfTheDay;

    public DashboardViewModel()
    {
        _dataService = new JsonDataService();
        LoadStatistics();
    }

    private void LoadStatistics()
    {
        var ingredients = _dataService.LoadIngredients();
        TotalIngredients = ingredients.Count;
        
        AvailableIngredients = ingredients.Count(i => i.IsAvailable);

        var recipes = _dataService.LoadRecipes();
        TotalRecipes = recipes.Count;

        if (recipes.Count > 0)
        {
            RecipeOfTheDay = recipes.FirstOrDefault();
        }
    }
}