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
using Zadanie1ClientAgentV1.ModelsDb;

namespace Zadanie1ClientAgentV1
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditClient.xaml
    /// </summary>
    public partial class AddAndEditClient : Window
    {
        public AddAndEditClient()
        {
            InitializeComponent();
        }
        Zadanieee1Context _db = new Zadanieee1Context();
        Client _client;
        private void AddOrEditClient_Loaded(object sender, RoutedEventArgs e)
        {
            if(Data.client == null)
            {
                ClientAddEdit.Title = "Добавление записи";
                btnAddEdit.Content = "Добавить";
                _client = new Client();
            }
            else
            {
                ClientAddEdit.Title = "Редактирование записи";
                btnAddEdit.Content = "Редактировать";
                _client = _db.Clients.Find(Data.client.Id);
            }
            ClientAddEdit.DataContext = _client;
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddEdit_Clicked(object sender, RoutedEventArgs e)
        {
           StringBuilder errors = new StringBuilder();
            if (tbClientEmail.Text.Length ==0 && tbClientPhone.Text.Length == 0 )
                errors.AppendLine("Обязательно должно быть указана почта или телефон");
            if (tbClientPhone.Text.Length == 0)
            {
                _client.Phone = null;
            }
            else if (double.TryParse(tbClientPhone.Text, out double phone))
            {
                _client.Phone = phone;
            }
            else
            {
                errors.AppendLine("Некорректный номер");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if(Data.client == null)
                {
                    _db.Clients.Add(_client);
                    _db.SaveChanges();
                }
                else
                {
                  _db.SaveChanges();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                _db.Clients.Remove(_client);
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
