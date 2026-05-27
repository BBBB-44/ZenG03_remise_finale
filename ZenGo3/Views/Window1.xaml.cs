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
using System.Windows.Shapes;
using ZenGo3.ViewModels;

namespace ZenGo3
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window 
    {
        public Window1() 
        {
            InitializeComponent();

            // Set the DataContext to the GameStats instance for data binding
            DataContext = new Window1VM(this);
        }

        private void SettingsUC_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
