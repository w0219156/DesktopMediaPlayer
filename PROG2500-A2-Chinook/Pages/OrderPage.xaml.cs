using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using PROG2500_A2_Chinook.Models;
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
                                   .Include(customer => customer.Invoices)
                                   .ThenInclude(invoice => invoice.InvoiceLines)
                                   .Where(customer => string.IsNullOrEmpty(search) || (customer.FirstName + " " + customer.LastName).Contains(search))
                                   .Select(customer => new
                                   {
                                       FullName = customer.LastName + ", " + customer.FirstName,
                                       // Conditional to handle empty locations
                                       Location = string.IsNullOrEmpty(customer.State) ? $"{customer.City}" : $"{customer.City}, {customer.State}",
                                       customer.Country,
                                       customer.Email,
                                       Invoices = customer.Invoices.Select(invoice => new
                                       {
                                           InvoiceDate = invoice.InvoiceDate,
                                           TotalAmount = invoice.Total,
                                           TrackCount = invoice.InvoiceLines.Count
                                       }).ToList()  // Convert to a list
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
