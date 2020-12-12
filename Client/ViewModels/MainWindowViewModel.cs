using Client.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel : IClient
    {
        #region Fields
        private BindingList<User> users;
        private User selectedUser;
        private ICentralAuthServer authServerProxy;
        private IMonitoringServer monitoringServerProxy;
        private IConnectionManager connectionManager;

        private ICentralAuthServer centralAuthServer;
        #endregion

        #region CTOR
        public MainWindowViewModel(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            ConnectAuthToServer();
            AuthenticateToAuthServer();
            Users = GetUsersFromServer();
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

        #region Misc methods
        private void ConnectAuthToServer()
        {
            while (true)
            {
                try
                {
                    centralAuthServer = connectionManager.GetAuthServerProxy();
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occured connecting to Central auth server.Reconnecting in 3 seconds..");
                    Console.WriteLine(e.Message);
                }
                Thread.Sleep(3000);
            }
            
        }

        private void AuthenticateToAuthServer()
        {
            string ownIp = "something"; //Add adequate values once we figure out where we will get them from
            string ownPort = "something";


            try
            {
                centralAuthServer.Authenticate(ownIp, ownPort);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                ConnectAuthToServer();
                AuthenticateToAuthServer();
            }

            
        }

        private BindingList<User> GetUsersFromServer()
        {
            try
            {
                return new BindingList<User>(authServerProxy.GetAllUsers());
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                ConnectAuthToServer();
                return GetUsersFromServer();
            }
        }

        public void RevocateCertificate()
        {
            try
            {
                authServerProxy.RevocateCertificate();
                MessageBox.Show("Revocation Successfull!");
            }
            catch(Exception e)
            {
                MessageBox.Show("Error occured revoking certificate. Try again in a few minutes.");
            }
        }
        #endregion

        #region Implemented interface
        public void sendMessage(string message)
        {
            throw new NotImplementedException();
        }
        #endregion


    }

}
