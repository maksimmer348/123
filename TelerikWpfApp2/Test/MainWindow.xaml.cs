using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }

    public class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<T>(ref T field, T newValue = default, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            field = newValue;
            if (propertyName != null)
            {
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }

    public class QualityControlNavigationItemModel : Base
    {
        public string Title { get; set; }
        public UserControl TypeUserControl { get; set; }
        public ObservableCollection<QualityControlNavigationItemModel> SubItems { get; set; }

        private bool isExpanded = false;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; SetField(ref isExpanded, value); }
        }

    }

    public class MainWindowViewModel : Base
    {
        private ObservableCollection<QualityControlNavigationItemModel> _menu = new ObservableCollection<QualityControlNavigationItemModel>();
        public ObservableCollection<QualityControlNavigationItemModel> Menu
        {
            get => _menu;
            set => SetField(ref _menu, value);
        }

        private QualityControlNavigationItemModel _menuItem;
        public QualityControlNavigationItemModel MenuItem
        {
            get => _menuItem;
            set
            {
                if (value != null)
                {
                    SetField(ref _menuItem, value);
                    ViewContent = value.TypeUserControl;
                }
            }
        }


        private UserControl _viewContent;
        public UserControl ViewContent
        {
            get => _viewContent;
            set => SetField(ref _viewContent, value);
        }

        private UserControlViewer1 UserControlViewer1 { get; set; } = new UserControlViewer1();
        private UserControlViewer2 UserControlViewer2 { get; set; } = new UserControlViewer2();
        private UserControlViewer3 UserControlViewer3 { get; set; } = new UserControlViewer3();

        public DelegateCommand CtrlCommand { get; private set; }

        public MainWindowViewModel()
        {
            MenuSet();
            CtrlCommand = new DelegateCommand(Ctrl);
        }

        private void MenuSet()
        {
            Menu.Add(new QualityControlNavigationItemModel
            {
                Title = "Viewer1",
                TypeUserControl = UserControlViewer1,
            });

            Menu.Add(new QualityControlNavigationItemModel
            {
                Title = "Hierarchy",
                SubItems = new ObservableCollection<QualityControlNavigationItemModel>
                {
                    new QualityControlNavigationItemModel
                    {
                        Title           = "Viewer2",
                        TypeUserControl = UserControlViewer2,
                    },
                    new QualityControlNavigationItemModel
                    {
                        Title           = "Viewer3",
                        TypeUserControl = UserControlViewer3,
                    },
                }
            });
        }

        private void Ctrl(object obj)
        {
            var title = (string)obj;
            if (title == "Viewer1")
            {
                MenuItem = Menu[0];
                ViewContent = UserControlViewer1;
            }
            else if (title == "Viewer2")
            {
                var parentItem = Menu[1];
                var subItem = parentItem.SubItems[0];
                MenuItem = subItem;
                parentItem.IsExpanded = true;
                ViewContent = UserControlViewer2;
            }
            else if (title == "Viewer3")
            {
                var parentItem = Menu[1];
                var subItem = parentItem.SubItems[1];
                MenuItem = subItem;
                parentItem.IsExpanded = true;
                ViewContent = UserControlViewer2;
            }
        }
    }
}
