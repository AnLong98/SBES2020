using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class RevocateCertificateCommand : ICommand
    {
        private MainWindowViewModel receiver;

        public RevocateCertificateCommand(MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true; //Can always be executed
        }

        public void Execute(object parameter)
        {
            receiver.RevocateCertificate();
        }
    }
}
