using System;
using System.Windows.Input;

namespace VTSWeb.Presentation.Common
{
    public class DelegateCommand : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;

        public DelegateCommand(Action<object> executeAction, 
            Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool temp = canExecute(parameter);

            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return canExecuteCache;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
