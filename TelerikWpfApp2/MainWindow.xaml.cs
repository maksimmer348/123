using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace TelerikWpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        private bool swapTheme;

        static MainWindow()
        {
            IsWindowsThemeEnabled = false;
        }
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Expression_DarkTheme();
            InitializeComponent();
        }

        private void cardView_Loaded(object sender, RoutedEventArgs e)
        {
            //var view = (QueryableCollectionView)cardView.ItemsSource;
            //var groupToCollapse = view.Groups[0];
            //cardView.Collapse(groupToCollapse);
        }

        protected void OnPropertyChanged(string porpName)
        {
            var temp = PropertyChanged;
            if (temp != null)
                temp(this, new PropertyChangedEventArgs(porpName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string FullName
        {
            get { return "Hello world"; }
        }
    }
}
