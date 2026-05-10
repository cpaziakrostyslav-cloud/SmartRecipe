using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartRecipe.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDarkMode;
    
    public List<string> Languages { get; } = new() { "Українська", "English" };

    [ObservableProperty]
    private string _selectedLanguage;

    public SettingsViewModel()
    {
        if (Application.Current != null)
        {
            var currentTheme = Application.Current.ActualThemeVariant;
            _isDarkMode = currentTheme == ThemeVariant.Dark;
        }
        
        _selectedLanguage = Languages[0];
    }

    partial void OnIsDarkModeChanged(bool value)
    {
        if (Application.Current != null)
        {
            Application.Current.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
        }
    }
    partial void OnSelectedLanguageChanged(string value)
    {
        if (Application.Current != null)
        {
            string uriString = value == "English" 
                ? "avares://SmartRecipe/Resources/Lang-EN.axaml" 
                : "avares://SmartRecipe/Resources/Lang-UK.axaml";
            
            var include = new ResourceInclude(new Uri("avares://SmartRecipe/App.axaml"))
            {
                Source = new Uri(uriString)
            };
            Application.Current.Resources.MergedDictionaries[0] = include;
        }
    }
}