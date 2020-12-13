using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Commands
{
    public class SendMessageCommand : ICommand
    {
        private ChatWindowViewModel receiver;

        public SendMessageCommand(ChatWindowViewModel receiver)
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
            string text = (parameter as string);
            return text != null && text != string.Empty;
        }

        public void Execute(object parameter)
        {
            receiver.SendMessage(parameter as string);
        }
    }
}
