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

namespace rand.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewSale.xaml
    /// </summary>
    public partial class AddNewSale : Window
    {
        Agent _agent;
        public AddNewSale(Agent agent)
        {
            _agent = agent;
            InitializeComponent();
            cbProducts.ItemsSource = RandomEntities.GetContext.Product.ToList();
        }

        private void tbQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductSale sale = new ProductSale
            {
                ProductionId = (cbProducts.SelectedItem as Product).Id,
                AgentId = _agent.Id,
                DateOfSale = DateTime.Now,
                Quantity = int.Parse(tbQuantity.Text)
            };
            _agent.ProductSale.Add(sale);
            RandomEntities.GetContext.SaveChanges();
            MessageBox.Show("Успешно добавлено");
            Close();
        }
    }
}
