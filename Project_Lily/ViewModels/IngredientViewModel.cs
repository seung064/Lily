using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Lily.ViewModels
{
    class IngredientViewModel : INotifyPropertyChanged
    {
        private Models.Ingredients model = null;
        public ICommand Production { get; set; }

        public IngredientViewModel()
        {
            model = new Models.Ingredients();
            Production = new RelayCommand(Execute_func, CanExecute_func);
        }

        public Models.Ingredients Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged("Model"); }
        }

        private void Execute_func(object obj)
        {
            model.IngredientName = "New Ingredient Name"; // Example action
        }

        private bool CanExecute_func(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand implementation for ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
