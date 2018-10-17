using System;
using System.Windows.Input;

namespace MultiSelectListViewMVVM
{
    /// <summary>
    /// Class for handling command from WPF control
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        /// Constructor for RelayCommand class
        /// </summary>
        /// <param name="canExecute">Predicate if the command can be executed.</param>
        /// <param name="execute">Action associated with the command.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">Data used by the command. Null if no data is required by the command.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return (_canExecute == null || _canExecute(parameter));
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. Null if no data is required by the command.</param>
        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
        }
    }
}
