﻿using Client.Commands;
using Client.Contracts;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow(string chatPeer, string chatUser)
        {
            ChatWindowViewModel viewModel = new ChatWindowViewModel(chatUser, chatPeer);
            viewModel.SendMessageCommand = new SendMessageCommand(viewModel);
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}