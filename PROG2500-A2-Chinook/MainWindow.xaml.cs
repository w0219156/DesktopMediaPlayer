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

namespace PROG2500_A2_Chinook
{
    //scaffold-dbcontext "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -outputdir Models/Generated -contextdir Data -namespace PROG2500_A2_Chinook.Models -contextnamespace PROG2500_A2_Chinook.Data -force
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Start on the home page
            mainFrame.NavigationService.Navigate(new PROG2500_A2_Chinook.Pages.HomePage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.HomePage());
        }

        private void AlbumsButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.AlbumsPage());
        }

        private void ArtistsButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.ArtistsPage());
        }

        private void TrackssButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.TracksPage());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
