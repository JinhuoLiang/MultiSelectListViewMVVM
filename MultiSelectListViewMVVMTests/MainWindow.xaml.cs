using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using MultiSelectListViewMVVM;

namespace MultiSelectListViewMVVMTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetListForUpDownListViewWithMVVM();
        }

        public UpDownListViewViewModel MvvmViewModel { set; get; }

        /// <summary>
        /// Set the listed items for the UpDownListView in WPF with Prism in  MVVM
        /// </summary>
        private void SetListForUpDownListViewWithMVVM()
        {
            // Create a list of items to be displayed in the UpDownListView control.
            List<Object> objects = new List<Object>
                {
                    new DataObjectForTesting("Item 1"),
                    new DataObjectForTesting("Item 2"),
                    new DataObjectForTesting("Item 3"),
                    new DataObjectForTesting("Item 4"),
                    new DataObjectForTesting("Item 5"),
                    new DataObjectForTesting("Item 6"),
                    new DataObjectForTesting("Item 7"),
                    new DataObjectForTesting("Item 8"),
                    new DataObjectForTesting("Item 9"),
                    new DataObjectForTesting("Item 10")
                };

            // Link the DataContext to the view model.
            MvvmViewModel = new UpDownListViewViewModel(objects);
            upDownListViewMVVM.DataContext = MvvmViewModel;
            listBoxForMVVM.DataContext = MvvmViewModel;
        }
    }
}
