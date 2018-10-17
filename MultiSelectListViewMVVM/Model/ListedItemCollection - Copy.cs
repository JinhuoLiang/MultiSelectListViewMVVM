using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UpDownListViewMVVM
{
    public class ListItem : PropertyChangedObject
    {
        private readonly Object _value;

        public ListItem(Object value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        private bool _isSelected;
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
        private ObservableCollection<ListItem> UpDownListItems;
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
                return UpDownListItems;
            }

            set
            {
                UpDownListItems = value;
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
        /// Set/get the selected items associated with the list view
        /// </summary>
        public ObservableCollection<ListItem> SelectedListItems { get; }
                = new ObservableCollection<ListItem>();

        /// <summary>
        /// Set/get all objects in this list
        /// </summary>
        public List<Object> Objects
        {
            //get { return new List<Object>(m_ListItems); }

            set
            {
                if (value == null)
                {
                    ListItems = null;
                    return;
                }

                UpDownListItems = new ObservableCollection<ListItem>();

                foreach (var item in value)
                    UpDownListItems.Add(new ListItem(item));

                ListItems = UpDownListItems;
            }
        }

        /// <summary>
        /// Move the selected item up one index
        /// </summary>
        public void MoveUp()
        {
            //if (ListItems == null || ListItems.Count < 2 || SelectedListItem == null)
            //    return;

            //var selectedIndex = ListItems.IndexOf(SelectedListItem);
            //if (selectedIndex > 0)
            //{
            //    var itemToMoveUp = ListItems[selectedIndex];
            //    ListItems.RemoveAt(selectedIndex);
            //    --selectedIndex;
            //    ListItems.Insert(selectedIndex, itemToMoveUp);
            //    SelectedListItem = itemToMoveUp;
            //}

            MoveUpSelectedItems(-1);
        }

        /// <summary>
        /// Move the selected item down one index
        /// </summary>
        public void MoveDown()
        {
            //if (ListItems == null || ListItems.Count < 2 || SelectedListItem == null)
            //    return;

            //var selectedIndex = ListItems.IndexOf(SelectedListItem);
            //if (selectedIndex < ListItems.Count - 1)
            //{
            //    var itemToMoveUp = ListItems[selectedIndex];
            //    ListItems.RemoveAt(selectedIndex);
            //    ++selectedIndex;
            //    ListItems.Insert(selectedIndex, itemToMoveUp);
            //    SelectedListItem = itemToMoveUp;
            //}
            MoveDownSelectedItems(-1);
        }

        private void MoveUpSelectedItems(int numSteps = 1)
        {
            if (UpDownListItems == null || UpDownListItems.Count == 0)
                return;

            var selectedIndex = 0;
            var count = UpDownListItems.Count;
            var isFirstSelected = true;

            for (int j = 0; j < count; j++)
            {
                selectedIndex = j;
                ListItem item = UpDownListItems[selectedIndex];

                if (item.IsSelected == false)
                    continue;

                if (isFirstSelected && numSteps < 0)
                    numSteps = j;

                var destIndex = j - numSteps;
                if (destIndex < 0)
                    return;

                for (int i = 0; i < numSteps; i++)
                {
                    var itemToMoveUp = UpDownListItems[destIndex];
                    UpDownListItems.RemoveAt(destIndex);
                    UpDownListItems.Insert(j, itemToMoveUp);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numSteps">Moving steps. -1 means to the top or bottom.</param>
        private void MoveDownSelectedItems(int numSteps = 1)
        {
            if (UpDownListItems == null || UpDownListItems.Count == 0)
                return;

            var selectedIndex = 0;
            var count = UpDownListItems.Count;
            var isFirstSelected = true;

            for (int j = 0; j < count; j++)
            {
                selectedIndex = count - j - 1;
                ListItem item = UpDownListItems[selectedIndex];

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
                    var itemToMoveUp = UpDownListItems[destIndex];
                    UpDownListItems.RemoveAt(destIndex);
                    UpDownListItems.Insert(destIndex - 1, itemToMoveUp);
                }
            }
        }
    }
}