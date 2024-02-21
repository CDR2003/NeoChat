using ChatShared.Data;
using Orleans.Concurrency;

namespace ChatGrainInterfaces;

public interface IUserObserver : IGrainObserver
{
    [OneWay]
    Task ReceiveMessage( string connectionId, ChatMessage message );
}