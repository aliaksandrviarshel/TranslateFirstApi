namespace TranslateFirstApi.TranslateFirstHubFeature.Services;

public interface IUsersConnectionsService
{
    void Add(Guid userId, string connectionId);
    string GetConnectionId(Guid userId);
    Guid GetUserId(string connectionId);
    void Remove(string connectionId);
}