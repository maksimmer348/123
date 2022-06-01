using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace TelerikWpfApp2
{
    public class NavigationViewItemModel
    {
        public StatusTest Status { get; set; }
        public string Icon{ get; set; }
        public Brush IconBrush { get; set; }
        public string Title { get; set; }
        public ObservableCollection<NavigationViewItemModel> SubItems { get; set; }
        public ICommand NavCommand { get; set; }
    }
}