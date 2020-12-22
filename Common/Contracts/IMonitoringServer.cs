///////////////////////////////////////////////////////////
//  IMonitoringServer.cs
//  Implementation of the Interface IMonitoringServer
//  Generated by Enterprise Architect
//  Created on:      12-Dec-2020 4:19:34 PM
//  Original author: Predrag
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceModel;

[ServiceContract]
public interface IMonitoringServer  {

    /// 
    /// <param name="sender"></param>
    /// <param name="receiver"></param>
    /// <param name="timestamp"></param>
    /// <param name="message"></param>
    [OperationContract]
    void LogCommunication(string sender, string receiver, string timestamp, string message);

    /// 
    /// <param name="sender"></param>
    /// <param name="receiver"></param>
    /// <param name="timestamp"></param>
    [OperationContract]
    void LogCommunicationEnd(string sender, string receiver);

    /// 
    /// <param name="sender"></param>
    /// <param name="receiver"></param>
    /// <param name="timestamp"></param>
    [OperationContract]
    void LogCommunicationStart(string sender, string receiver);
}//end IMonitoringServer