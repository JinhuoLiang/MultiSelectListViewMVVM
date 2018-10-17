# MultiSelectListViewMVVM
Building a User Control with User Defined Order Multiple Selected List in WPF and MVVM

This user control allows all selected items to be moved up or down one position or to the top or bottom position. Because WPF does not support data binding for multiple selection, a new class with a property called 'IsSelected' in the data model is defined for each item in the ListView control. In this way, the listed items in the ListView control are bound to the ObservableCollection f ListItem in the data model with each listed item bound to the ListItem's 'IsSelected' property. 
