using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TelerikWpfApp2.Model.Navigation
{
    public class NAvi
    {
        public ObservableCollection<string> Glyph { get; set; }

        public string Title { get; set; }

        public UserControl TypeUserControl { get; set; }

        public ObservableCollection<NAvi> SubItems { get; set; }
    }
}
