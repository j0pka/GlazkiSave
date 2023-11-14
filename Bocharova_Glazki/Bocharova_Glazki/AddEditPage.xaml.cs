using Microsoft.Win32;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent _currentAgent = new Agent();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                this._currentAgent = SelectedAgent; //_currentAgent = SelectedAgent;
                ComboType.SelectedIndex = _currentAgent.AgentTypeID  - 1;
                Console.WriteLine(_currentAgent.AgentTypeID + 1);//отображать текущий тип
            }
            if (SelectedAgent == null)
            {
                DelButton.Visibility = Visibility.Hidden; 
                DelButton.IsEnabled = false;
            }
                DataContext = _currentAgent;
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myoOpenFileDialog = new OpenFileDialog();
            if (myoOpenFileDialog.ShowDialog() == true)
            {
                _currentAgent.Logo = myoOpenFileDialog.FileName; //сделать так, чтобы пусть файла стал относительным, пока что абсолютный
                LogoImage.Source = new BitmapImage(new Uri(myoOpenFileDialog.FileName));
            }
        }

        private void SaveBth_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
            {
                errors.AppendLine("Укажите наименование агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
            {
                errors.AppendLine("Укажите адрес агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
            {
                errors.AppendLine("Укажите ФИО директора");
            }
            if (ComboType.SelectedItem == null)
            {
                errors.AppendLine("Укажите тип агента");
            }
            
            /* if (ComboType.SelectedItem == null)
            {
                errors.AppendLine("Укажите тип агента");
            }*/
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
            {
                errors.AppendLine("Укажите приоритет агента");
            }
            if (_currentAgent.Priority <= 0)
            {
                errors.AppendLine("Укажите положительный приоритет агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
            {
                errors.AppendLine("Укажите ИНН агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
            {
                errors.AppendLine("Укажите КПП агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
            {
                errors.AppendLine("Укажите телефон агента");
            }

            else
            {
                string ph = _currentAgent.Phone;
                string cleanPhoneNumber = new string(ph.Where(char.IsDigit).ToArray());

                if ((cleanPhoneNumber.StartsWith("9") || cleanPhoneNumber.StartsWith("4") || cleanPhoneNumber.StartsWith("8")) && cleanPhoneNumber.Length == 11)
                {
                    // Номер для префиксов 9, 4, 8 и длины 11
                }
                else if (cleanPhoneNumber.StartsWith("3") && cleanPhoneNumber.Length == 12)
                {
                    // Номер для префикса 3 и длины 12
                }
                else
                {
                    errors.AppendLine("Укажите правильно телефон агента");
                }
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
            {
                errors.AppendLine("Укажите почту агента");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentAgent.ID == 0)
            {
                _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;//
                //воозможно

                Bocharova_GlazkiEntities.GetContext().Agent.Add(_currentAgent);
            }

            Console.WriteLine(_currentAgent.Title);
            Console.WriteLine(_currentAgent.ID);
            Console.WriteLine(_currentAgent.AgentTypeID); 
            Console.WriteLine(_currentAgent.AgentType);
            //Console.WriteLine(_currentAgent.AgentTypeString);
            try
            {
                if (_currentAgent.ID != 0)
                {
                    var existingAgent = Bocharova_GlazkiEntities.GetContext().Agent.Find(_currentAgent.ID);

                    if (existingAgent != null)
                    {
                        // Обновляем поля
                        existingAgent.Title = _currentAgent.Title;
                        existingAgent.Address = _currentAgent.Address;
                        existingAgent.DirectorName = _currentAgent.DirectorName;
                        existingAgent.Priority = _currentAgent.Priority;
                        existingAgent.INN = _currentAgent.INN;
                        existingAgent.KPP = _currentAgent.KPP;
                        existingAgent.Phone = _currentAgent.Phone;
                        existingAgent.Email = _currentAgent.Email;

                        // Обновляем изображение
                        existingAgent.Logo = _currentAgent.Logo;

                        // Обновляем тип агента
                        _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1; 

                        existingAgent.AgentTypeID = _currentAgent.AgentTypeID;
                        

                        // Сохраняем изменения в базе данных
                        Bocharova_GlazkiEntities.GetContext().SaveChanges();

                        MessageBox.Show("Информация обновлена");
                        Manager.MainFrame.GoBack();
                    }
                }
                else
                {
                    // Если ID равен 0, то это новая запись
                    _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;

                    // Добавляем новую запись
                    Bocharova_GlazkiEntities.GetContext().Agent.Add(_currentAgent);

                    // Сохраняем изменения в базе данных
                    Bocharova_GlazkiEntities.GetContext().SaveChanges();

                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }




            Console.WriteLine(_currentAgent.Title);
            Console.WriteLine(_currentAgent.ID);
            Console.WriteLine(_currentAgent.AgentTypeID); Console.WriteLine(_currentAgent.AgentType);
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var _currentAgent = (sender as Button).DataContext as Agent;
            if (_currentAgent == null) return;

            var Sale = Bocharova_GlazkiEntities.GetContext().ProductSale.ToList().Where(p => _currentAgent.ID == p.AgentID).ToList().Count;
            if (Sale != 0)
            {
                MessageBox.Show("Невозможно выполнить удаление, у агента существует реализация продукта");
                return;
            }

            if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var userToDelete = Bocharova_GlazkiEntities.GetContext().Agent.Where(p => p.ID == _currentAgent.ID).FirstOrDefault();
                try
                {
                    Bocharova_GlazkiEntities.GetContext().Agent.Remove(userToDelete);
                    Bocharova_GlazkiEntities.GetContext().SaveChanges();
                   
                    MessageBox.Show("Информация удалена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
    }
}
