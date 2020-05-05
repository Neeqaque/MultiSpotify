using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MultiSpotify.Annotations;

namespace MultiSpotify.UserControls
{
    /// <summary>
    /// Interaction logic for EditableImage.xaml
    /// </summary>
    public partial class EditableImage : UserControl
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(EditableImage));

        public static readonly DependencyProperty SourceProperty = 
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(EditableImage));

        public static readonly DependencyProperty IsEditEnabledProperty =
            DependencyProperty.Register("IsEditEnabled", typeof(bool), typeof(EditableImage));

        public ImageSource Source
        {
            get => (ImageSource) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool IsEditEnabled
        {
            get => (bool) GetValue(IsEditEnabledProperty);
            set => SetValue(IsEditEnabledProperty, value);
        }

        public EditableImage()
        {
            InitializeComponent();
        }
    }
}
