namespace Client.Contracts
{
    public interface IConnectionManager
    {
        ICentralAuthServer GetAuthServerProxy();
        IClient GetClientProxy(string clientIP, string clientPort, string clientUserName);
        IMonitoringServer GetMonitorProxy();
    }
}