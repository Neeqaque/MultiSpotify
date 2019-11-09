using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MultiSpotify.ViewModel;

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

        public static void SetTheme(Themes theme)
        {
            for (int i = 0; i < Application.Current.Resources.MergedDictionaries.Count; i++)
            {
                if (Application.Current.Resources.MergedDictionaries[i].Source == new Uri(lightTheme, UriKind.Relative) ||
                    Application.Current.Resources.MergedDictionaries[i].Source == new Uri(darkTheme, UriKind.Relative))
                {
                    Application.Current.Resources.MergedDictionaries.Remove(Application.Current.Resources.MergedDictionaries[i]);
                    i--;
                }
            }

            ResourceDictionary chosenTheme = new ResourceDictionary();

            switch (theme)
            {
                case Themes.Dark:
                    chosenTheme.Source = new Uri(darkTheme, UriKind.Relative);
                    CurrentTheme = Themes.Dark;
                    break;
                case Themes.Light:
                    chosenTheme.Source = new Uri(lightTheme, UriKind.Relative);
                    CurrentTheme = Themes.Light;
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Add(chosenTheme);
        }
    }
}
