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

namespace Bocharova_Glazki
{
    /// <summary>
    /// Логика взаимодействия для EyesPage1.xaml
    /// </summary>
    public partial class EyesPage1 : Page
    {
        public EyesPage1()
        {
            InitializeComponent();
            var currentService = Bocharova_GlazkiEntities.GetContext().Product.ToList();
            EyesListView.ItemsSource = currentService;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

       
    }
}
