using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SmartRecipe.Models;

namespace SmartRecipe.Services;

public class JsonDataService
{
    private readonly string _recipesFilePath = "recipes_data.json";
    private readonly string _ingredientsFilePath = "ingredients_data.json";
    
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public void SaveRecipes(IEnumerable<Recipe> recipes)
    {
        string json = JsonSerializer.Serialize(recipes, _options);
        File.WriteAllText(_recipesFilePath, json);
    }

    public List<Recipe> LoadRecipes()
    {
        if (!File.Exists(_recipesFilePath))
        {
            return new List<Recipe>();
        }
        string json = File.ReadAllText(_recipesFilePath);
        return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
    }

    public void SaveIngredients(IEnumerable<Ingredient> ingredients)
    {
        string json = JsonSerializer.Serialize(ingredients, _options);
        File.WriteAllText(_ingredientsFilePath, json);
    }

    public List<Ingredient> LoadIngredients()
    {
        if (!File.Exists(_ingredientsFilePath))
        {
            return new List<Ingredient>();
        }

        string json = File.ReadAllText(_ingredientsFilePath);
        return JsonSerializer.Deserialize<List<Ingredient>>(json) ?? new List<Ingredient>();
    }
}