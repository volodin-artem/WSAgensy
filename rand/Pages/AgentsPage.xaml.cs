using Microsoft.VisualBasic;
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

namespace rand.Pages
{
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        public AgentsPage()
        {
            InitializeComponent();
            lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        void Update()
        {
            string search = tbSearch.Text.ToLower(); ;
            IEnumerable<Agent> items = null;
            if (cbFilter.SelectedIndex == 3 || cbSort.SelectedItem == null)
            {
                items = RandomEntities.GetContext.Agent.ToList();
                lbAgents.ItemsSource = items.Where(x => x.Name.ToLower().Contains(search) || x.Phone.Contains(search) || x.Email.ToLower().Contains(search));
                return;
            }
            if (cbFilter.SelectedIndex == 0 || cbSort.SelectedIndex == 0)
            {
                items = RandomEntities.GetContext.Agent.ToList().OrderBy(x => x.Name);
                if (cbSort.SelectedIndex == 1)
                {
                    items = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Name);
                }
            }
            if(cbSort.SelectedIndex == 1 && cbFilter.SelectedItem == null)
            {
                items = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Name);
            }
            if (cbFilter.SelectedIndex == 1)
            {
                items = RandomEntities.GetContext.Agent.ToList().OrderBy(x => x.Sale);
                if (cbSort.SelectedIndex == 1)
                {
                    items = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Sale);

                }
            }

            if (cbFilter.SelectedIndex == 2)
            {
                items = RandomEntities.GetContext.Agent.ToList().OrderBy(x => x.Priority);
                if (cbSort.SelectedIndex == 1)
                {
                    items = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Priority);
                }
            }
            lbAgents.ItemsSource = items.Where(x => x.Name.ToLower().Contains(search) || x.Phone.Contains(search) || x.Email.ToLower().Contains(search));
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void lbAgents_Selected(object sender, RoutedEventArgs e)
        {
            if (lbAgents.SelectedItems.Count == 0)
            {
                buttChangePr.Visibility = Visibility.Collapsed;
            }
            else buttChangePr.Visibility = Visibility.Visible;
        }

        private void Butt_Click(object sender, RoutedEventArgs e)
        {
            var items = lbAgents.SelectedItems.Cast<Agent>().ToList();
            int val = items.Max(x => x.Priority);
            bool parse = int.TryParse(Interaction.InputBox("Введите новое значение приоритета"), out val);
            items.ForEach(x => x.Priority += val);
            RandomEntities.GetContext.SaveChanges();
            string value = parse ? $"Успех" : $"Вы ввели не числовое значение, приоритет установлен на {val}";
            MessageBox.Show(value);
            Update();
        }

        private void lbAgents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Agent agent = lbAgents.SelectedItem as Agent;
            if(agent != null)
            new EditAgent(agent).ShowDialog();
            Update();
        }
    }
}
