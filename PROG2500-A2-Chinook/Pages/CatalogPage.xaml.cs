using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using PROG2500_A2_Chinook.Data;
using System.Windows;
using PROG2500_A2_Chinook.Models;

namespace PROG2500_A2_Chinook.Pages
{
    public partial class CatalogPage : Page
    {
        private ChinookContext context = new ChinookContext();

        public CatalogPage()
        {
            InitializeComponent();
            LoadCatalogData();
        }

        private void LoadCatalogData(string search = "")
        {
            var artists = context.Artists
                                 .Include(albums => albums.Albums)
                                 .ThenInclude(al => al.Tracks)
                                 .Where(albums => string.IsNullOrEmpty(search) || albums.Name.Contains(search))
                                 .OrderBy(albums => albums.Name)
                                 .ToList();
            var groupedData = artists
                .GroupBy(albums => albums.Name.Substring(0, 1).ToUpper()) // Get the first letter


                // Anonymous type to count the number of artists in the grtoup
                // referenced https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types
                
                .Select(group => new
                {
                    Key = $"{group.Key} ({group.Count()} Artists)",
                    Artists = group.Select(artist => new
                    {
                        ArtistName = $"{artist.Name} ({artist.Albums.Count} Albums)",
                        Albums = artist.Albums.Select(album => new
                        {
                            AlbumTitle = $"{album.Title} ({album.Tracks.Count} Tracks)",
                            Tracks = album.Tracks.Select(track => track.Name).ToList()
                        }).ToList()  // Converting to a list, same below
                    }).ToList()
                }).ToList();

            var collectionViewSource = (CollectionViewSource)this.FindResource("groupedCatalog");
            collectionViewSource.Source = groupedData;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadCatalogData(searchBox.Text);
        }
    }
}