using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
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

namespace PROG2500_A2_Chinook.Pages
{
    /// <summary>
    /// Interaction logic for ArtistsPage.xaml
    /// </summary>
    public partial class ArtistsPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource artistsVS = new CollectionViewSource();
        public ArtistsPage()
        {
            InitializeComponent();

            artistsVS = (CollectionViewSource)FindResource(nameof(artistsVS));

            context.Artists.Load();

            artistsVS.Source = context.Artists.Local.ToObservableCollection();

        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            var searchTerm = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                artistsVS.Source = context.Artists.Local.ToObservableCollection();
            }
            else
            {
                var filteredArtists = context.Artists
                    .Where(a => a.Name.ToLower().Contains(searchTerm))
                    .ToList();

                artistsVS.Source = filteredArtists;
            }
        }
    }
}
