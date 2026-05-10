using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartRecipe.Models;
using SmartRecipe.Services;

namespace SmartRecipe.ViewModels;

public partial class RecipesViewModel : ViewModelBase
{
    private readonly JsonDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Recipe> _recipes;
    
    [ObservableProperty] private bool _isAddFormVisible;
    [ObservableProperty] private string _newRecipeName = string.Empty;
    [ObservableProperty] private string _newRecipeDescription = string.Empty;
    [ObservableProperty] private string _newRecipeIngredients = string.Empty;

    public RecipesViewModel()
    {
        _dataService = new JsonDataService();
        LoadRecipes();
        CalculateMatchStatuses();
    }

    private void LoadRecipes()
    {
        var loaded = _dataService.LoadRecipes();
        
        if (loaded.Count > 0)
        {
            Recipes = new ObservableCollection<Recipe>(loaded);
        }
        else
        {
            Recipes = new ObservableCollection<Recipe>
            {
                new Recipe 
                { 
                    Name = "Класичний омлет", 
                    Description = "Швидкий сніданок з яєць та молока. Ідеально підходить для початку дня.", 
                    RequiredIngredients = new List<string> { "Яйця", "Молоко" }
                },
                new Recipe 
                { 
                    Name = "Смажена курка з цибулею", 
                    Description = "Соковите куряче філе, обсмажене до золотистої скоринки.", 
                    RequiredIngredients = new List<string> { "Куряче філе", "Цибуля" }
                }
            };
            _dataService.SaveRecipes(Recipes);
        }
    }

    private void CalculateMatchStatuses()
    {
        var availableIngredients = _dataService.LoadIngredients()
            .Where(i => i.IsAvailable)
            .Select(i => i.Name.ToLower().Trim())
            .ToList();
        
        foreach (var recipe in Recipes)
        {
            var missingIngredients = recipe.RequiredIngredients
                .Where(req => !availableIngredients.Contains(req.ToLower().Trim()))
                .ToList();
            
            if (missingIngredients.Count == 0)
            {
                recipe.MatchStatus = "Готово до приготування";
                recipe.StatusColor = "#27AE60";
            }
            else
            {
                recipe.MatchStatus = $"Не вистачає інгредієнтів: {missingIngredients.Count}";
                recipe.StatusColor = "#E67E22"; 
            }
        }
    }
    [RelayCommand]
    private void ToggleAddForm()
    {
        IsAddFormVisible = !IsAddFormVisible;
    }
    
    [RelayCommand]
    private void AddNewRecipe()
    {
        if (string.IsNullOrWhiteSpace(NewRecipeName) || string.IsNullOrWhiteSpace(NewRecipeIngredients))
        {
            return;
        }
        
        var ingredientsList = NewRecipeIngredients
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(i => i.Trim())
            .ToList();

        var newRecipe = new Recipe
        {
            Name = NewRecipeName.Trim(),
            Description = NewRecipeDescription.Trim(),
            RequiredIngredients = ingredientsList
        };
        
        Recipes.Add(newRecipe);
        _dataService.SaveRecipes(Recipes);
        CalculateMatchStatuses();
        
        NewRecipeName = string.Empty;
        NewRecipeDescription = string.Empty;
        NewRecipeIngredients = string.Empty;
        IsAddFormVisible = false;
    }
    [RelayCommand]
    private void DeleteRecipe(Recipe recipeToDelete)
    {
        if (recipeToDelete != null)
        {
            Recipes.Remove(recipeToDelete);
            _dataService.SaveRecipes(Recipes);
        }
    }
}