using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PROG2500_A2_Chinook.Models;

namespace PROG2500_A2_Chinook.Pages
{
    
    /// <summary>
    /// Interaction logic for TracksPage.xaml
    /// </summary>
    public partial class TracksPage : Page
    {
        ChinookContext context = new ChinookContext();
        CollectionViewSource tracksVS = new CollectionViewSource();

        
        public TracksPage()
        {
            InitializeComponent();

            tracksVS = (CollectionViewSource)FindResource(nameof(tracksVS));

            context.Tracks.Load();


            // Bring in Album data to display
            context.Tracks.Include(track => track.Album).Load();

            tracksVS.Source = context.Tracks.Local.ToObservableCollection();
        }

        private void OnSearchTrackClick(object sender, RoutedEventArgs e)
        {
            var searchTerm = txtSearchTrack.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                var tracks = context.Tracks.Include(t => t.Album).ToList();
                tracksVS.Source = new ObservableCollection<Track>(tracks);
            }
            else
            {
                var filteredTracks = context.Tracks
                                            .Include(t => t.Album)
                                            .Where(t => t.Name.ToLower().Contains(searchTerm))
                                            .ToList();

                tracksVS.Source = new ObservableCollection<Track>(filteredTracks);
            }
        }
    }
}
