namespace TranslateFirstApi.WaitingRoomFeature.Waiter.NicknameGeneration;

public interface INicknameGenerator
{
    Task<string> Generate();
}