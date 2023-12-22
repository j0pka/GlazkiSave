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
    /// Логика взаимодействия для CosrHistoryPage.xaml
    /// </summary>
    public partial class CosrHistoryPage : Page
    {

        Agent currentAgent;

        public CosrHistoryPage(Agent SelectedAgent)
        {
            InitializeComponent();
            currentAgent = SelectedAgent;
            var Sales = Bocharova_GlazkiEntities.GetContext().ProductSale.ToList();
            if (SelectedAgent.ID != 0)
            {
                Console.WriteLine(SelectedAgent.ID);
                Sales = Sales.Where(p => p.AgentID == SelectedAgent.ID).ToList();
            }
            HistoryCostListView.ItemsSource = Sales;
            DeleteCostHistory.Visibility = Visibility.Collapsed;
        }

        private void Update_Sales()
        {
            var Sales = Bocharova_GlazkiEntities.GetContext().ProductSale.ToList();
            if (currentAgent.ID != 0)
            {
                Sales = Sales.Where(p => p.AgentID == currentAgent.ID).ToList();
            }
            HistoryCostListView.ItemsSource = Sales;
        }

        private void AddCostHistory_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddRecordPage(currentAgent));
        }

        private void DeleteCostHistory_Click(object sender, RoutedEventArgs e)
        {
            List<ProductSale> SelectedSales = HistoryCostListView.SelectedItems.Cast<ProductSale>().ToList();
            foreach (ProductSale Sale in SelectedSales)
            {
                Bocharova_GlazkiEntities.GetContext().ProductSale.Remove(Sale);
            }
            Bocharova_GlazkiEntities.GetContext().SaveChanges();
            Update_Sales();
        }

        private void HistoryCostListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryCostListView.SelectedItems.Count == 0)
                DeleteCostHistory.Visibility = Visibility.Collapsed;
            if (HistoryCostListView.SelectedItems.Count > 0)
                DeleteCostHistory.Visibility = Visibility.Visible;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update_Sales();
        }
    }
}
