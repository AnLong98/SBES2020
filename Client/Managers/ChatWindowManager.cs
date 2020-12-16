using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Managers
{
    public static class ChatWindowManager
    {
        public static void CreateNewForInitiating(string chatPeerUsername, string currentUser, IClient peerProxy)
        {
            ChatWindowViewModel viewModel = new ChatWindowViewModel(currentUser, chatPeerUsername, peerProxy);
            MessageNotificationManager.Instance().AddReceiver(viewModel, chatPeerUsername);
            new ChatWindow(viewModel).Show();
        }

        public static void CreateNewForIncoming(string chatPeerUsername, string currentUser, IClient peerProxy)
        {
            ChatWindowViewModel viewModel = new ChatWindowViewModel(currentUser, chatPeerUsername, peerProxy);
            MessageNotificationManager.Instance().AddReceiver(viewModel, chatPeerUsername);
            new ChatWindow(viewModel).Show();
        }
    }
}
