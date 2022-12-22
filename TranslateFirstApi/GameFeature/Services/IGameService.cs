using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.WaitingRoomFeature;

namespace TranslateFirstApi.GameFeature.Services;
public interface IGameService
{
    Task<GameDto> GetGame(Guid userId, Guid gameId);
    Task StartGame(WaitingRoom waitingRoom);
    Task<UserAnsweredDto> Answer(Guid userId, Guid translateId);
    Task Leave(Guid userId);
}