using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Project_Lily.Commands
{

    public class RelayCommand : ICommand
    {



        private readonly Action<object?> execute;
        private readonly Predicate<object?>? canExecute; // 생성자에서만 값이 설정 가능하며 T타입을 

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => execute(parameter);


        public event EventHandler? CanExecuteChanged  // UI에 변경사항 알림
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}