using System;
using System.Collections.Generic;

namespace SmartRecipe.Models;

public class Recipe
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> RequiredIngredients { get; set; } = new();
}