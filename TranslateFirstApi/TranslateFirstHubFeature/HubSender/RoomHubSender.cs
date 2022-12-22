using Microsoft.AspNetCore.SignalR;
using TranslateFirstApi.TranslateFirstHubFeature;
using TranslateFirstApi.TranslateFirstHubFeature.Services;

namespace TranslateFirstApi.TranslateFirstHubFeature.HubSender;

public class RoomHubSender : IRoomHubSender
{
    private readonly IHubContext<TranslateFirstHub> _hubContext;
    private readonly IUsersConnectionsService _usersConnectionsService;
    private Dictionary<Guid, string> _userIdConnectionIdPairs = new();

    public RoomHubSender(IHubContext<TranslateFirstHub> hubContext, IUsersConnectionsService usersConnectionsService)
    {
        _hubContext = hubContext;
        _usersConnectionsService = usersConnectionsService;
    }

    public void AddUser(Guid userId)
    {
        var connectionId = _usersConnectionsService.GetConnectionId(userId);
        _userIdConnectionIdPairs.Add(userId, connectionId);
    }

    public void RemoveUser(Guid userId)
    {
        _userIdConnectionIdPairs.Remove(userId);
    }

    public Task ToAllAsync(string method, object? arg)
    {
        //Console.WriteLine("To all " + string.Join(", ", _userIdConnectionIdPairs.Select(x => (x.Key, x.Value))));
        return _hubContext.Clients.Clients(_userIdConnectionIdPairs.Values).SendAsync(method, arg);
    }

    public Task ToAllExceptAsync(Guid userId, string method, object? arg)
    {
        var connectionIds = _userIdConnectionIdPairs.Where(x => x.Key != userId).Select(x => x.Value);
        return _hubContext.Clients.Clients(connectionIds).SendAsync(method, arg);
    }
}
