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
    /// Логика взаимодействия для AddEditAgent.xaml
    /// </summary>
    public partial class AddEditAgent : Window
    {
        public AddEditAgent()
        {
            InitializeComponent();
        }
        Zadanieee1Context _db = new Zadanieee1Context();
        Agent _agent;
        private void AddOrEditAgent_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.agent == null)
            {
                AgentAddEdit.Title = "Добавление записи";
                btnAddEditAgent.Content = "Добавить";
                _agent = new Agent();
            }
            else
            {
                AgentAddEdit.Title = "Редактирование записи";
                btnAddEditAgent.Content = "Редактировать";
                _agent = _db.Agents.Find(Data.agent.Id);
            }
            AgentAddEdit.DataContext = _agent;
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddEditAgent_Clicked(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if(tbFirstName.Text.Length == 0) error.AppendLine("Напишите Фамилию");
            if (tbMiddleName.Text.Length == 0) error.AppendLine("Напишите Имя");
            if (tbLastName.Text.Length == 0) error.AppendLine("Напишите Отчество");
            if (tbDealShare.Text.Length == 0)
            {
                _agent.DealShare = null;
            }
            else if (int.TryParse(tbDealShare.Text, out int dealShare))
            {
                if (dealShare >= 0 && dealShare <= 100)
                {
                    _agent.DealShare = dealShare;
                }
                else
                {
                    error.AppendLine("Некоректная доля от комиссии");
                }
            }
            

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            try
            {
                if (Data.agent == null)
                {
                    _db.Agents.Add(_agent);
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
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
