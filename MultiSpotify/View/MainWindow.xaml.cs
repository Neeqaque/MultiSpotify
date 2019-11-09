using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MultiSpotify.Properties;
using MultiSpotify.Source;

namespace MultiSpotify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ThemeManager.SetTheme(ThemeManager.Themes.Dark);

            Maximize_Button.Content = WindowState == WindowState.Maximized
                ? App.Current.Resources["RestoreIcon"]
                : App.Current.Resources["MaximizeIcon"];

            SourceInitialized += MainWindow_SourceInitialized;
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            WindowSizing.WindowInitialized(this);
        }

        private void Header_Panel_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            Hide();
        }

        private void Maximize_Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindowState();
        }

        private void ChangeWindowState()
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                Maximize_Button.Content = App.Current.Resources["MaximizeIcon"];
            }
            else
            {
                WindowState = WindowState.Maximized;
                Maximize_Button.Content = App.Current.Resources["RestoreIcon"];
            }
        }

        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        Stopwatch sw = new Stopwatch();
        private bool clicked = false;
        private void Header_Panel_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!clicked)
            {
                clicked = true;
                sw.Start();
            }
            else
            {
                if (sw.ElapsedMilliseconds < 200)
                {
                    ChangeWindowState();
                    sw.Reset();
                    clicked = false;
                }
                else
                {
                    sw.Reset();
                    sw.Start();
                }
            }
        }

        private void TaskbarIcon_OnTrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Show();
            TaskBarRestore_Button.Content = "Minimize To Tray";
        }

        private void TaskBarExit_Button_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TaskBarRestore_Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (Visibility == Visibility.Hidden)
            {
                Show();
                TaskBarRestore_Button.Content = "Minimize To Tray";
            }
            else
            {
                Hide();
                TaskBarRestore_Button.Content = "Show MultiSpotify";
            }

            TaskbarIcon.TrayPopupResolved.IsOpen = false;
        }
    }
}
