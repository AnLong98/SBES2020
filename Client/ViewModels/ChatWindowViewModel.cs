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
    public class ChatWindowViewModel : IMessageNotifiable, INotifyPropertyChanged
    {
        #region Fields
        private string messages = "";
        private IClient messageReceiver;
        private IMonitoringServer monitoringServerProxy;
        private IAESSecurity aesSecurity;
        private string chatUserName;
        private string chatPeerUserName;
        private string inputText;
        #endregion

        #region CTOR
        public ChatWindowViewModel(string chatUserName, string chatPeerUserName, IClient peerProxy, IMonitoringServer monitoringServerProxy)
        {
            this.chatUserName = chatUserName;
            this.chatPeerUserName = chatPeerUserName;
            this.messageReceiver = peerProxy;
            this.Messages += $"You are now connected to {chatPeerUserName}";
            this.monitoringServerProxy = monitoringServerProxy;
            //new Task(MockChatting).Start();
        }
        #endregion

        #region  Commands
        public ICommand SendMessageCommand { get; set; }
        #endregion

        #region Test Helping Mock
        private void MockChatting()
        {
            for(int i = 0; i < 10; i++)
            {
                Notify($"Hello #{i}");
                Thread.Sleep(10000);
            }
        }
        #endregion

        #region Get Set
        public ICloseable WindowContext { get; set; }

        public string Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                OnPropertyChanged("Messages");
            }
        }

        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                OnPropertyChanged("InputText");
            }
        }
        #endregion

        #region Property changed event
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Misc methods

        public void SendMessage(string message)
        {
            string key = ""; //Get key somehow
            //string encryptedMessage = aesSecurity.Encrypt(message, key);

            try
            {
                messageReceiver.SendMessage(message);
                Messages += $"\n[{chatUserName}]: " + message;
                InputText = "";
                
            }catch(Exception e)
            {
                MessageBox.Show("Message couldn't be sent!");
                //LogCommunicationEnd();
            }
            //LogCommunication(encryptedMessage);
        }
        #endregion

        #region Monitoring server Logging
        private void LogCommunicationEnd()
        {
            try
            {
                monitoringServerProxy.LogCommunicationEnd(chatUserName, chatPeerUserName, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            }
            catch
            { 
                WindowContext.Close(); 
            }
        }

        private void LogCommunication(string encryptedMessage)
        {
            try
            {
                monitoringServerProxy.LogCommunication(chatUserName, chatPeerUserName, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), encryptedMessage);
            }
            catch
            {
                WindowContext.Close();
            }
        }
        #endregion

        #region Implemented interface
        public void Notify(string message)
        {
            Messages += $"\n[{chatPeerUserName}]: " + message;
        }
        #endregion
    }
}
