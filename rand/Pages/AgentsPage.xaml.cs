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
            string search = tbSearch.Text;
            if (cbFilter.SelectedIndex == 3 || cbSort.SelectedItem == null)
            {
                lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().Where(x => x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));
                return;
            }
            if (cbFilter.SelectedIndex == 0)
            {
                lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().OrderBy(x => x.Name).Where(x => x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));
                if (cbSort.SelectedIndex == 1)
                {
                    lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Name).Where(x=> x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));

                }
            }

            if (cbFilter.SelectedIndex == 1)
            {
                lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().OrderBy(x => x.Sale).Where(x => x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));
                if (cbSort.SelectedIndex == 1)
                {
                    lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Sale).Where(x => x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));

                }
            }

            if (cbFilter.SelectedIndex == 2)
            {
                lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().OrderBy(x => x.Priority).Where(x => x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));
                if (cbSort.SelectedIndex == 1)
                {
                    lbAgents.ItemsSource = RandomEntities.GetContext.Agent.ToList().OrderByDescending(x => x.Priority).Where(x => x.Name.Contains(search) || x.Phone.Contains(search) || x.Email.Contains(search));

                }
            }
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
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Priority = val;
            }
            RandomEntities.GetContext.SaveChanges();
            string value = parse ? $"Успех" : $"Вы ввели не числовое значение, приоритет установлен на {val}";
            MessageBox.Show(value);
            Update();
        }
    }
}
