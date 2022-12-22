namespace TranslateFirstApi.TranslateFirstHubFeature.Services;

public class UsersConnectionsService : IUsersConnectionsService
{
    private Dictionary<Guid, string> _userIdConnectionIdPairs = new ();

    public void Add(Guid userId, string connectionId)
    {
        _userIdConnectionIdPairs.Add(userId, connectionId);
    }

    public string GetConnectionId(Guid userId)
    {
        return _userIdConnectionIdPairs.GetValueOrDefault(userId) ?? throw new Exception($"Connection id associated with user id: {userId} was not found");
    }

    public Guid GetUserId(string connectionId)
    {
        return _userIdConnectionIdPairs.First(x => x.Value == connectionId).Key;
    }

    public void Remove(string connectionId)
    {
        _userIdConnectionIdPairs = _userIdConnectionIdPairs.Where(x => x.Value != connectionId).ToDictionary(x => x.Key, x => x.Value);
    }
}
