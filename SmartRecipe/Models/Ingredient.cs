using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartRecipe.Models;

public partial class Ingredient : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private bool _isAvailable;
    
    public Ingredient(string name, bool isAvailable = false)
    {
        Name = name;
        IsAvailable = isAvailable;
    }
}