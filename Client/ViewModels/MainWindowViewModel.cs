using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel
    {
        #region Fields
        private BindingList<User> users;
        private User selectedUser;
        #endregion

        #region CTOR
        public MainWindowViewModel(BindingList<User> users)
        {
            Users = users;
        }

        //Testing CTOR
        public MainWindowViewModel()
        {
            Users = MockUsers();
        }
        #endregion

        #region Commands
        public ICommand RevocateCertficicateCommand { get; set; }
        public ICommand StartChatCommand { get; set; }
        #endregion

        #region Getters and Setters
        public BindingList<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }

        }

        #endregion

        private BindingList<User> MockUsers()
        {
            BindingList<User> users = new BindingList<User>();
            users.Add(new User("192.168.1.1", "42", "Mika"));
            users.Add(new User("192.168.1.2", "45", "Zika"));
            users.Add(new User("192.168.1.3", "44", "Kika"));
            users.Add(new User("192.168.1.4", "41", "Bika"));

            return users;
        }

        #region Property changed event
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }

}
