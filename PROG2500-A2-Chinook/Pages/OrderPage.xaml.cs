using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PROG2500_A2_Chinook.Pages
{
    public partial class OrderPage : Page
    {
        private ChinookContext context = new ChinookContext();

        public OrderPage()
        {
            InitializeComponent();
            LoadCustomerOrders();
        }

        private void LoadCustomerOrders(string search = "")
        {
            var customers = context.Customers
                                   .Include(c => c.Invoices)
                                   .ThenInclude(i => i.InvoiceLines)
                                   .Where(c => string.IsNullOrEmpty(search) || (c.FirstName + " " + c.LastName).Contains(search))
                                   .Select(c => new
                                   {
                                       FullName = c.LastName + ", " + c.FirstName,
                                       Location = string.IsNullOrEmpty(c.State) ? $"{c.City}" : $"{c.City}, {c.State}",
                                       c.Country,
                                       c.Email,
                                       Invoices = c.Invoices.Select(i => new
                                       {
                                           InvoiceDate = i.InvoiceDate,
                                           TotalAmount = i.Total,
                                           TrackCount = i.InvoiceLines.Count
                                       }).ToList()
                                   })
                                   .ToList();

            var collectionViewSource = (CollectionViewSource)this.FindResource("customersViewSource");
            collectionViewSource.Source = customers;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerOrders(searchBox.Text);
        }
    }
}
