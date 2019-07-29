using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiSpotify.Source
{
    public static class Icons
    {
        public static string Home = (string)App.Current.FindResource("HomeIcon");
        public static string More = (string)App.Current.FindResource("MoreIcon");
        public static string ChevronDown = (string)App.Current.FindResource("ChevronDownIcon");
        public static string Add = (string)App.Current.FindResource("AddIcon");
        public static string Cancel = (string)App.Current.FindResource("CancelIcon");
        public static string Search = (string)App.Current.FindResource("SearchIcon");
        public static string ChevronLeft = (string)App.Current.FindResource("ChevronLeftIcon");
        public static string ChevronRight = (string)App.Current.FindResource("ChevronRightIcon");
        public static string Radio = (string)App.Current.FindResource("RadioIcon");
        public static string Library = (string)App.Current.FindResource("LibraryIcon");
        public static string Volume0 = (string)App.Current.FindResource("Volume0Icon");
        public static string Volume1 = (string)App.Current.FindResource("Volume1Icon");
        public static string Volume2 = (string)App.Current.FindResource("Volume2Icon");
        public static string Volume3 = (string)App.Current.FindResource("Volume3Icon");
        public static string Heart = (string)App.Current.FindResource("HeartIcon");
        public static string HeartFilled = (string)App.Current.FindResource("HeartFilledIcon");
        public static string Browse = (string)App.Current.FindResource("BrowseIcon");
    }
}
