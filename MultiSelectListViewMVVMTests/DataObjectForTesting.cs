using System;

namespace MultiSelectListViewMVVMTests
{
    public class DataObjectForTesting : Object
    {
        /// <summary>
        /// Cconstructor of data object with name
        /// </summary>
        /// <param name="name">Name of the data object</param>
        public DataObjectForTesting(string name = null)
        {
            Name = name;
        }

        /// <summary>
        /// Name of the data object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Over write ToString() with the name of the data object
        /// </summary>
        /// <returns>Name of the data object</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

