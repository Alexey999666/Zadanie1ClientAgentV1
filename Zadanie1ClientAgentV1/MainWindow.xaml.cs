using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zadanie1ClientAgentV1.ModelsDb;

namespace Zadanie1ClientAgentV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void btnAddClient_Clicked(object sender, RoutedEventArgs e)
        {
            Data.client = null;
            AddAndEditClient AddEditClient = new AddAndEditClient();
            AddEditClient.Owner = this;
            AddEditClient.ShowDialog();
            LoadDbClientInDG();

        }

        private void btnUpdateClient_Clicked(object sender, RoutedEventArgs e)
        {
            if(DGClient.SelectedItem != null)
            {
                Data.client = (Client)DGClient.SelectedItem;
                AddAndEditClient AddEditClient = new AddAndEditClient();
                AddEditClient.Owner = this;
                AddEditClient.ShowDialog();
                LoadDbClientInDG();
            }
        }

        private void btnDeleteClient_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Client row = (Client)DGClient.SelectedItem;
                    if (row != null)
                    {
                        using (Zadanieee1Context _db = new Zadanieee1Context())
                        {
                            _db.Clients.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDbClientInDG();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");

                }
            }
            else DGClient.Focus();
        }

        

        private void DBLoaded_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDbClientInDG();
        }
        void LoadDbClientInDG()
        {
            using (Zadanieee1Context _db = new Zadanieee1Context())
            {
                int SelectedIndex = DGClient.SelectedIndex;
                DGClient.ItemsSource = _db.Clients.ToList();
              

            }
        }

        private void tcClientFindLastName_Changed(object sender, TextChangedEventArgs e)
        {
            List<Client> LastNameItems = (List<Client>)DGClient.ItemsSource;
            var findLastName = LastNameItems.Where(p => p.LastName?.Contains(tbClientFindLastName.Text) == true);
            if (findLastName.Count() > 0)
            {
                var item = findLastName.First();
                DGClient.SelectedItem = item;
                DGClient.ScrollIntoView(item);
                
               
            }
        }

        private void tcClientFindPhone_Changed(object sender, TextChangedEventArgs e)
        {
            
            List<Client> PhoneItems = (List<Client>)DGClient.ItemsSource;
            var findPhone = PhoneItems.FirstOrDefault(p => p.Phone?.ToString().Contains(tbClientFindPhone.Text) == true);
            if (findPhone != null)
            {
              
                DGClient.SelectedItem = findPhone;
                DGClient.ScrollIntoView(findPhone);
               
            }
        }

        private void tcClientFindEmail_Changed(object sender, TextChangedEventArgs e)
        {
            List<Client> EmailItems = (List<Client>)DGClient.ItemsSource;
            var findEmail = EmailItems.Where(p => p.Email?.Contains(tbClientFindEmail.Text) == true);
            if (findEmail.Count() > 0)
            {
                var item = findEmail.First();
                DGClient.SelectedItem = item;
                DGClient.ScrollIntoView(item);
                

            }
            
        }
    }
}