using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmartRecipe.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private object _currentPage;

    public MainWindowViewModel()
    {
        CurrentPage = new FridgeViewModel();
    }
    
    [RelayCommand]
    private void NavigateToFridge()
    {
        CurrentPage = new FridgeViewModel();
    }
    
    [RelayCommand]
    private void NavigateToRecipes()
    {
        CurrentPage = new RecipesViewModel();
    }
    
    [RelayCommand]
    private void NavigateToSettings()
    {
        CurrentPage = new SettingsViewModel();
    }
}