using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiSpotify.Source
{
    class Command : ICommand
    {
        private bool _canExecute = true;
        private Action _action;
        private Action<object> _parameterizedAction;

        public Command(Action action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public Command(Action<object> parameterizedAction, bool canExecute = true)
        {
            _parameterizedAction = parameterizedAction;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            if (!_canExecute) return;

            if (_action != null)
            {
                _action();
            }
            else
            {
                _parameterizedAction(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
