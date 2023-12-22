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

namespace Bocharova_Glazki
{
    /// <summary>
    /// Логика взаимодействия для PriorityWin.xaml
    /// </summary>
    public partial class PriorityWin : Window
    {
        public PriorityWin(int max)
        {
            InitializeComponent();
            PrioritiEdit.Text = max.ToString();
        }
        private void SavePriorityWin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PrioritiEdit.Text))
                Close();                   // 
            else
                MessageBox.Show("Введите новый приоритет агентов", "Ошибка!");
        }

        private void BtnBackPriorityWin_Click(object sender, RoutedEventArgs e)
        {
            PrioritiEdit.Text = " ";
            Close();
        }
    }
}
