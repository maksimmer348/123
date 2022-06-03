using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using static System.Diagnostics.Debug;

namespace TelerikWpfApp2;

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
        DataContext = new ViewModel();
        Trace.Write("start");
    }

    private void RadNavigationView_OnItemClick(object sender, RoutedEventArgs e)
    {
        var clickedItem = e.OriginalSource as RadNavigationViewItem;
        Trace.Write($"{clickedItem?.Content}");
        (DataContext as ViewModel).SS();
    }

    private void OnNavigationViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            var currentlySelectedItem = e.AddedItems[0] as NavigationViewItemModel;
            Trace.Write($"{currentlySelectedItem.Title}");
        }
    }
}


