using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartRecipe.Models;

public partial class Recipe : ObservableObject
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> RequiredIngredients { get; set; } = new();
    [ObservableProperty]
    private string _matchStatus = string.Empty;
    
    [ObservableProperty]
    private string _statusColor = "Gray";
}