using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.WaitingRoomFeature.Waiter;

namespace TranslateFirstApi.WaitingRoomFeature.Services;
public interface IWaitingRoomService
{
    Task<WaitingRoomDto> CreateWaitingRoom(WaitingUser user);
    Task<WaitingRoomDto> JoinWaitingRoom(string code, WaitingUser user);
    Task StartGame(string code);
    void Leave(Guid userId);
}