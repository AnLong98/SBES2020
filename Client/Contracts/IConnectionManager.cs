namespace Client.Contracts
{
    public interface IConnectionManager
    {
        ICentralAuthServer GetAuthServerProxy();
        IClient GetClientProxy(string clientIP, string clientPort);
        IMonitoringServer GetMonitorProxy();
    }
}