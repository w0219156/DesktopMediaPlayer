using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using PROG2500_A2_Chinook.Data;
using System.Windows;

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
                                 .Include(a => a.Albums)
                                 .ThenInclude(al => al.Tracks)
                                 .Where(a => string.IsNullOrEmpty(search) || a.Name.Contains(search))
                                 .OrderBy(a => a.Name)
                                 .ToList();

            var groupedData = artists
                .GroupBy(a => a.Name.Substring(0, 1).ToUpper())
                .Select(g => new
                {
                    Key = $"{g.Key} ({g.Count()} Artists)",
                    Artists = g.Select(artist => new
                    {
                        ArtistName = artist.Name,
                        Albums = artist.Albums.Select(album => new
                        {
                            AlbumTitle = album.Title,
                            Tracks = album.Tracks.Select(track => track.Name).ToList()
                        }).ToList()
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