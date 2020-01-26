using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MultiSpotify.UserControls
{
    /// <summary>
    /// Interaction logic for LeftMenuControl.xaml
    /// </summary>
    public partial class LeftMenuControl : UserControl
    {
        public LeftMenuControl()
        {
            InitializeComponent();
        }

        private void Item_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                MenuScrollViewer.LineUp();
            }
            else
            {
                MenuScrollViewer.LineDown();
            }
        }
    }
}
