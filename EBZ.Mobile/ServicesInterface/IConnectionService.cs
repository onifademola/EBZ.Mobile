using Plugin.Connectivity.Abstractions;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
