using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
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
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;
        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public EyesPage1()
        {
            InitializeComponent();
            

            sortComboBox.SelectedIndex = 0;///////////
            filterComboBox.SelectedIndex = 0;

            var _currentAgents = Bocharova_GlazkiEntities.GetContext().Agent.ToList();
            EyesListView.ItemsSource = _currentAgents;

            UpdateAgents();
        }

        /// <summary>
        /// 
        /// </summary>


        public void UpdateAgents()
        {
            var _currentAgents = Bocharova_GlazkiEntities.GetContext().Agent.ToList();

            int selectedType = filterComboBox.SelectedIndex;

            if (sortComboBox.SelectedIndex == 0)
            {
                _currentAgents = Bocharova_GlazkiEntities.GetContext().Agent.ToList();
            }

            if (sortComboBox.SelectedIndex == 1)
            {
                //currentAgents = currentAgents.OrderBy(p => p.Discount).ToList();
            }

            if (sortComboBox.SelectedIndex == 2)
            {
                //currentAgents = currentAgents.OrderByDescending(p => p.Discount).ToList();
            }

            if (sortComboBox.SelectedIndex == 3)
            {
                _currentAgents = _currentAgents.OrderBy(p => p.Priority).ToList();
            }

            if (sortComboBox.SelectedIndex == 4)
            {
                _currentAgents = _currentAgents.OrderByDescending(p => p.Priority).ToList();
            }

            if (sortComboBox.SelectedIndex == 5)
            {
                _currentAgents = _currentAgents.OrderBy(p => p.Title).ToList();
            }

            if (sortComboBox.SelectedIndex == 6)
            {
                _currentAgents = _currentAgents.OrderByDescending(p => p.Title).ToList();
            }

            if (selectedType != 0)
            {
                _currentAgents = _currentAgents.Where(p => p.AgentTypeID == selectedType).ToList();
            }
            else
            {
                _currentAgents = _currentAgents.Where(p => p.AgentTypeID != 0).ToList();
            }

            if (TBoxSearch.Text != "" && TBoxSearch.Text != "Введите для поиска")
            {
                int i = 0;
                Console.WriteLine(int.TryParse(TBoxSearch.Text, out i));

                _currentAgents = _currentAgents.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.Replace("+7", "8").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Contains(TBoxSearch.Text.Replace("+7", "8").Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""))).ToList();



            }

            //EyesListView = new ListView();//Запускает, но не работает поиск, сортировка и фильтрация
            EyesListView.ItemsSource = _currentAgents;

            TableList = _currentAgents;
            ChangePage(0, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));//
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents(); // ломается
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();//
        }

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();//
        }

        /* private void TBoxSearch_GotFocus(object sender, RoutedEventArgs e)
         {
             if (TBoxSearch.Text == "Введите для поиска")
             {
                 TBoxSearch.Text = "";
             }//
         }

         private void TBoxSearch_LostFocus(object sender, RoutedEventArgs e)
         {
             if (TBoxSearch.Text == "")
             {
                 TBoxSearch.Text = "Введите для поиска";
             }
         }//*/

        private void ComboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            noSort.Text = "Все";//
        }

        private void ComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            noSort.Text = "Сортировка";//
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Bocharova_GlazkiEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.State = EntityState.Detached);
                EyesListView.ItemsSource = Bocharova_GlazkiEntities.GetContext().Agent.AsNoTracking().ToList();
                /*
                Bocharova_GlazkiEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());//reload!!!!!!!!!!!!
                EyesListView.ItemsSource = Bocharova_GlazkiEntities.GetContext().Agent.ToList();*/
                UpdateAgents();
            }
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }
        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }
            Boolean Ifupdate = true;

            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for(int i=1; i<=CountPage;i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();

                EyesListView.ItemsSource = CurrentPageList;
                EyesListView.Items.Refresh();

            }
            
            
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void Real_Click(object sender, RoutedEventArgs e)
        {
            int max = 0;
            foreach (Agent AgentLV in EyesListView.SelectedItems)
            {
                if (AgentLV.Priority >= max) max = AgentLV.Priority;
            }
            PriorityWin window = new PriorityWin(max);
            window.ShowDialog();
            if (string.IsNullOrEmpty(window.PrioritiEdit.Text))
                return;
            MessageBox.Show(window.PrioritiEdit.Text);
            foreach (Agent AgentLV in EyesListView.SelectedItems)
                AgentLV.Priority = Convert.ToInt32(window.PrioritiEdit.Text);
            try
            {
                Bocharova_GlazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            UpdateAgents();
        }
    }
}
