using System.Collections.ObjectModel;

namespace TelerikWpfApp2
{
    public class NavigationViewItemModel
    {
        public string Icon{ get; set; }
        public string Title { get; set; }
        public ObservableCollection<NavigationViewItemModel> SubItems { get; set; }
    }
}