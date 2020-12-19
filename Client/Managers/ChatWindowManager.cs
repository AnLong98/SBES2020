using Client.Contracts;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Client.Managers
{
    public static class ChatWindowManager
    {
        private static readonly object lockCreate = new object();
        public static void CreateNewChatWindow(string chatPeerUsername, string currentUser, IClient peerProxy, IMonitoringServer monitoring, IAESSecurity security)
        {
            ChatWindowViewModel viewModel = new ChatWindowViewModel(currentUser, chatPeerUsername,peerProxy, monitoring, security);
            MessageNotificationManager.Instance().AddReceiver(viewModel, chatPeerUsername);
            lock (lockCreate)
            {

                Thread newChatThread = new Thread(() =>
                {

                    // Create our context, and install it:
                    SynchronizationContext.SetSynchronizationContext(
                        new DispatcherSynchronizationContext(
                            Dispatcher.CurrentDispatcher));

                    ChatWindow tempWindow = new ChatWindow(viewModel)
                    {
                        Title = chatPeerUsername
                    };
                    tempWindow.Closed += (s, e) =>
                        Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);

                    tempWindow.Show();

                    System.Windows.Threading.Dispatcher.Run();
                });
                newChatThread.SetApartmentState(ApartmentState.STA);
                newChatThread.IsBackground = true;
                newChatThread.Start();
            }
        }

    }
}
