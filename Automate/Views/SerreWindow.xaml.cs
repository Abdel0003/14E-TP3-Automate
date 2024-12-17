using Automate.ViewModels;
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

namespace Automate.Views
{
    /// <summary>
    /// Logique d'interaction pour SerreWindow.xaml
    /// </summary>
    public partial class SerreWindow : Window
    {
        public SerreWindow()
        {
            InitializeComponent();
            // Lier le ViewModel à la fenêtre
            DataContext = new SerreViewModel();
        }
    }
}
