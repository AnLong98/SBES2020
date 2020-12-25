using AESSecurity;
using Client.Commands;
using Client.Managers;
using Client.ViewModels;
using Common.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows;


namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string currentUser = WinLogonNameParser.ParseName(WindowsIdentity.GetCurrent().Name);
            MainWindowViewModel vm = new MainWindowViewModel(new ConnectionManager(), new ServiceHosts.ClientMessagingHost(), currentUser, new AESCryptographyProvider());
            vm.RevocateCertificateCommand = new RevocateCertificateCommand(vm);
            vm.StartChatCommand= new StartChatCommand(vm);
            vm.LoadCertificateCommand = new LoadCertificateCommand(vm);
            DataContext = vm;
            InitializeComponent();
        }
    }
}
