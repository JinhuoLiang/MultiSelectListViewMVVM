using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MultiSelectListViewMVVM
{
    /// <summary>
    /// This class is used to support multiple selection in the listview with data binding
    /// </summary>
    public class ListItem : PropertyChangedObject
    {
        private readonly Object _value;
        private bool _isSelected;

        /// <summary>
        /// Constructor with data object.
        /// </summary>
        /// <param name="value">Data object</param>
        public ListItem(Object value)
        {
            _value = value;
        }

        /// <summary>
        /// Text of list item in the listview
        /// </summary>
        /// <returns>Name of the data object</returns>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <summary>
        /// Data binding to selected/unselected list item
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
    }

    public class ListedItemCollection : PropertyChangedObject
    {
        private ObservableCollection<ListItem> m_ListItems;
        private ListItem m_SelectedListItem;

        /// <summary>
        /// Constructor with a list of objects to be displayed in the list view
        /// </summary>
        /// <param name="list">A list of objects to be displayed in the list view</param>
        public ListedItemCollection(List<Object> list = null)
        {
            Objects = list;
        }

        /// <summary>
        /// Set/get all items associated with the list view
        /// </summary>
        public ObservableCollection<ListItem> ListItems
        {
            get
            {
                return m_ListItems;
            }

            set
            {
                m_ListItems = value;
                NotifyPropertyChanged("ListItems");
            }
        }

        /// <summary>
        /// Set/get the selected item associated with the list view
        /// </summary>
        public ListItem SelectedListItem
        {
            get
            {
                return m_SelectedListItem;
            }

            set
            {
                m_SelectedListItem = value;
                NotifyPropertyChanged("SelectedListItem");
            }
        }

        /// <summary>
        /// Set/get all objects in this list
        /// </summary>
        public List<Object> Objects
        {
            set
            {
                if (value == null)
                {
                    ListItems = null;
                    return;
                }

                m_ListItems = new ObservableCollection<ListItem>();

                foreach (var item in value)
                    m_ListItems.Add(new ListItem(item));

                ListItems = m_ListItems;
            }
        }

        /// <summary>
        /// Move the selected item up one index
        /// </summary>
        public void MoveUp()
        {
            MoveUpSelectedItems();
        }

        /// <summary>
        /// Move the selected item down one index
        /// </summary>
        public void MoveDown()
        {
            MoveDownSelectedItems();
        }

        /// <summary>
        /// Move the selected item to the top
        /// </summary>
        public void MoveTop()
        {
            MoveUpSelectedItems(-1);
        }

        /// <summary>
        /// Move the selected item to the bottom
        /// </summary>
        public void MoveBottom()
        {
            MoveDownSelectedItems(-1);
        }

        /// <summary>
        /// Move up selected items in the listview
        /// </summary>
        /// <param name="numSteps">Moving steps. -1 means to the top or bottom.</param>
        private void MoveUpSelectedItems(int numSteps = 1)
        {
            if (ListItems == null || ListItems.Count == 0)
                return;

            var selectedIndex = 0;
            var count = ListItems.Count;
            var isFirstSelected = true;

            for (int j = 0; j < count; j++)
            {
                selectedIndex = j;
                ListItem item = ListItems[selectedIndex];

                if (item.IsSelected == false)
                    continue;

                if (isFirstSelected && numSteps < 0)
                    numSteps = j;

                var destIndex = j - numSteps;
                if (destIndex < 0)
                    return;

                for (int i = 0; i < numSteps; i++)
                {
                    var itemToMoveUp = ListItems[destIndex];
                    ListItems.RemoveAt(destIndex);
                    ListItems.Insert(j, itemToMoveUp);
                }
            }
        }

        /// <summary>
        /// Move down selected items in the listview
        /// </summary>
        /// <param name="numSteps">Moving steps. -1 means to the top or bottom.</param>
        private void MoveDownSelectedItems(int numSteps = 1)
        {
            if (ListItems == null || ListItems.Count == 0)
                return;

            var selectedIndex = 0;
            var count = ListItems.Count;
            var isFirstSelected = true;

            for (int j = 0; j < count; j++)
            {
                selectedIndex = count - j - 1;
                ListItem item = ListItems[selectedIndex];

                if (item.IsSelected == false)
                    continue;

                if (isFirstSelected && numSteps < 0)
                    numSteps = j;

                var destIndex = selectedIndex + numSteps;
                if (destIndex >= count)
                    return;

                for (int i = 0; i < numSteps; i++)
                {
                    destIndex = selectedIndex + i + 1;
                    var itemToMoveUp = ListItems[destIndex];
                    ListItems.RemoveAt(destIndex);
                    ListItems.Insert(destIndex - 1, itemToMoveUp);
                }
            }
        }
    }
}