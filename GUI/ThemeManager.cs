using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.Device;
using Themes = GUI.Resources.Themes;

namespace GUI
{
    public static class ThemeManager
    {
        private const string _themeKey = "theme";
        private static readonly IDictionary<string, ResourceDictionary> _themesDictionary = 
            new Dictionary<string, ResourceDictionary>
            {
                [nameof(Themes.Light)] = new Themes.Light(),
                [nameof(Themes.Dark)] = new Themes.Dark(),
                [nameof(Themes.Night)] = new Themes.Night(),
                //[nameof(GUI.Resources.Styles.CustomStyles)] = new GUI.Resources.Styles.CustomStyles(),
            };

        public static string SelectedTheme { get; set; } = nameof(Themes.Dark);



        static ThemeManager()
        {
            Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        public static void initialize()
        {
            var selectedTheme = Preferences.Default.Get<string>(_themeKey, null);
            
            if (selectedTheme is null)
            {
                //if (Application.Current.UserAppTheme == AppTheme.Unspecified)
                if (Application.Current.PlatformAppTheme == AppTheme.Unspecified)
                {
                    selectedTheme = nameof(Themes.Light);
                }
                //else if (Application.Current.UserAppTheme == AppTheme.Light)
                else if (Application.Current.PlatformAppTheme == AppTheme.Light)
                {
                    selectedTheme = nameof(Themes.Light);
                }
                else
                {
                    selectedTheme = nameof(Themes.Dark);
                }
            }

            SetTheme(selectedTheme);
        }

        private static void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            if (e.RequestedTheme == AppTheme.Dark)
            {
                SetTheme(nameof(Themes.Dark));
            }
            else if (e.RequestedTheme == AppTheme.Light)
            {
                SetTheme(nameof(Themes.Light));
            }
        }

        public static void SetTheme(string themeName)
        {
            if (SelectedTheme == themeName) { return; }

            var themeToBeApplied = _themesDictionary[themeName];

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(themeToBeApplied);
            
            //Application.Current.Resources.MergedDictionaries.Add((ResourceDictionary)GUI.Resources.MergedDictionaries.Styles.CustomStyles);

            SelectedTheme = themeName;

            Preferences.Default.Set(_themeKey, themeName);
        }









    }
}
