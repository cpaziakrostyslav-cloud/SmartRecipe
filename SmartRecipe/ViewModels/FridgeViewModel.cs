using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartRecipe.Models;
using SmartRecipe.Services;

namespace SmartRecipe.ViewModels;

public partial class FridgeViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Ingredient> _ingredients;

    public FridgeViewModel()
    {
        _dataService = new JsonDataService();
        
        var loadedIngredients = _dataService.LoadIngredients();

        if (loadedIngredients.Count > 0)
        {
            Ingredients = new ObservableCollection<Ingredient>(loadedIngredients);
        }
        else
        {
            Ingredients = new ObservableCollection<Ingredient>
            {
                new Ingredient("Куряче філе"),
                new Ingredient("Яйця", true),
                new Ingredient("Молоко"),
                new Ingredient("Борошно", true),
                new Ingredient("Помідори"),
                new Ingredient("Сир твердий"),
                new Ingredient("Картопля"),
                new Ingredient("Цибуля")
            };
        }
    }
    [RelayCommand]
    private void Save()
    {
        _dataService.SaveIngredients(Ingredients);
    }
}