using Client.Contracts;
using Client.ServiceHosts;
using Client.Views;
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
    public class MainWindowViewModel : IClient, INotifyPropertyChanged
    {
        #region Fields
        private BindingList<User> users;
        private ICentralAuthServer authServerProxy;
        private IMonitoringServer monitoringServerProxy;
        private IConnectionManager connectionManager;
        private ClientMessagingHost host;
        private ICentralAuthServer centralAuthServer;
        #endregion

        #region CTOR and Startup
        public MainWindowViewModel(IConnectionManager connectionManager, ClientMessagingHost host)
        {
            this.connectionManager = connectionManager;
            this.host = host;
            new Task(StartUp).Start();
        }

        private void StartUp()
        {
            host.Open();
            //ConnectAuthToServer();
            //AuthenticateToAuthServer();
            Users = MockUsers();
            //Users = GetUsersFromServer();
        }

        //Testing CTOR
        public MainWindowViewModel()
        {
            Users = MockUsers();
        }
        #endregion

        #region Commands
        public ICommand RevocateCertificateCommand { get; set; }
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

            try
            {
                centralAuthServer = connectionManager.GetAuthServerProxy();
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured connecting to Central auth server.");
                Console.WriteLine(e.Message);
            }
        }

        private void AuthenticateToAuthServer()
        {
            string ownIp = "something"; //Add adequate values once we figure out where we will get them from
            string ownPort = "something";

            while (true)
            {
                try
                {
                    centralAuthServer.Authenticate(ownIp, ownPort);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occured contacting Central auth server. Click OK to try again...");
                    ConnectAuthToServer();
                }
                Thread.Sleep(5000);
            }

            
        }

        private BindingList<User> GetUsersFromServer()
        {
            while (true)
            {
                try
                {
                    return new BindingList<User>(authServerProxy.GetAllUsers());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occured contacting Central auth server. Click OK to try again...");
                    ConnectAuthToServer();
                }
                Thread.Sleep(5000);
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

        #region Start Chat
        public void StartChat(User user)
        {
            new ChatWindow(user.Username, "Pedjica").Show();
        }
        #endregion


    }

}
