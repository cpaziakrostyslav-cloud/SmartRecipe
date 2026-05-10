using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartRecipe.Models;

namespace SmartRecipe.ViewModels;

public partial class FridgeViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Ingredient> _ingredients;

    public FridgeViewModel()
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