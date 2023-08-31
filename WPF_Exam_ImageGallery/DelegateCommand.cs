using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Exam_ImageGallery
{
    public class DelegateCommand : ICommand
    {
        Action<object> _execute;
        Predicate<object> _canExecute;
        public object Parameter { get; set; }

        protected internal object sender;

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
        }

        public bool CanExecute(object parameter)
        {
            Parameter = parameter;
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            Parameter = parameter;
            sender = parameter;
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
