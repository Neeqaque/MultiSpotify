using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiSpotify
{
    public static class ThemeManager
    {

        public static Themes CurrentTheme { get; private set; }

        public enum Themes
        {
            Light,
            Dark
        }

        private const string lightTheme = "ResourceDictionaries/Themes/LightTheme.xaml";
        private const string darkTheme = "ResourceDictionaries/Themes/DarkTheme.xaml";

        private const string styles = "ResourceDictionaries/Styles.xaml";

        public static void SetTheme(Themes theme)
        {
            Application.Current.Resources.Clear();

            ResourceDictionary dict = Application.LoadComponent(new Uri(styles, UriKind.Relative)) as ResourceDictionary;
            
            switch (theme)
            {
                case Themes.Dark:
                    dict.MergedDictionaries.Add(Application.LoadComponent(new Uri(darkTheme, UriKind.Relative)) as ResourceDictionary);
                    CurrentTheme = Themes.Dark;
                    break;
                case Themes.Light:
                    dict.MergedDictionaries.Add(Application.LoadComponent(new Uri(lightTheme, UriKind.Relative)) as ResourceDictionary);
                    CurrentTheme = Themes.Light;
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
