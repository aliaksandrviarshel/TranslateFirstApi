namespace TranslateFirstApi.TranslateFirstHubFeature.HubSender;

public interface IRoomHubSender
{
    void AddUser(Guid userId);
    void RemoveUser(Guid userId);
    Task ToAllAsync(string method, object? arg);
    Task ToAllExceptAsync(Guid userId, string method, object? arg);
}