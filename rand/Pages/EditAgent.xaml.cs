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
using Microsoft.Win32;
using rand;
using System.IO;

namespace rand.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditAgent.xaml
    /// </summary>
    public partial class EditAgent : Window
    {
        Agent _agent;
        public EditAgent(Agent agent)
        {
            _agent = agent;
            this.DataContext = agent;
            InitializeComponent();
            lbSale.ItemsSource = agent.ProductSale;
        }

        private void buttonChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false
            };
            if (dialog.ShowDialog() != false)
            {
                string path = dialog.FileName;
                string fileEx = path.Split('.').Last();
                string newFileName = @"\agents\agent" + new Random().Next(9999999) + "." + fileEx;
                File.Copy(path, Directory.GetCurrentDirectory() + newFileName);
                _agent.Logo = newFileName;
                RandomEntities.GetContext.SaveChanges();
                MessageBox.Show("Успешно сохранен!");
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + newFileName));
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            RandomEntities.GetContext.SaveChanges();
            MessageBox.Show("Успешно сохранен!");
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var sale = lbSale.SelectedItem as ProductSale;
            if(sale != null)
            {
                RandomEntities.GetContext.ProductSale.Remove(sale);
                RandomEntities.GetContext.SaveChanges();
                MessageBox.Show("Успешно сохранен!");
                lbSale.ItemsSource = _agent.ProductSale;
            }
            else MessageBox.Show("Выберите элемент!");
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddNewSale(_agent).ShowDialog();
            RandomEntities.GetContext.SaveChanges();
            lbSale.ItemsSource = new List<ProductSale>();
            var items = _agent.ProductSale;
            lbSale.ItemsSource = items;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        private void buttonDelete_Click_1(object sender, RoutedEventArgs e)
        {
            if (_agent.ProductSale.Count == 0)
            {
                RandomEntities.GetContext.Agent.Remove(_agent);
                Close();
            }
            else MessageBox.Show("У агента есть информация о реализации продукта, удаление запрещено");
        }
    }
}
