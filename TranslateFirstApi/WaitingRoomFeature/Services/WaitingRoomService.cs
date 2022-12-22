using Microsoft.AspNetCore.SignalR;
using TranslateFirstApi.GameFeature.Services;
using TranslateFirstApi.TranslateFirstHubFeature;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.TranslateFirstHubFeature.HubSender;
using TranslateFirstApi.TranslateFirstHubFeature.Services;
using TranslateFirstApi.WaitingRoomFeature.CodeGeneration;
using TranslateFirstApi.WaitingRoomFeature.Waiter;

namespace TranslateFirstApi.WaitingRoomFeature.Services;

public class WaitingRoomService : IWaitingRoomService
{
    readonly List<WaitingRoom> _waitingRooms = new();
    private readonly IGameService _gameService;
    private readonly IHubContext<TranslateFirstHub> _hubContext;
    private readonly IUsersConnectionsService _usersConnectionsService;

    public WaitingRoomService(IGameService gameService, IHubContext<TranslateFirstHub> hubContext, IUsersConnectionsService usersConnectionsService)
    {
        _gameService = gameService;
        _hubContext = hubContext;
        _usersConnectionsService = usersConnectionsService;
    }

    public async Task<WaitingRoomDto> CreateWaitingRoom(WaitingUser user)
    {
        var waitingRoom = new WaitingRoom(
            new CodeGenerator(),
            new RoomHubSender(
                _hubContext,
                _usersConnectionsService
                )
            );
        await waitingRoom.AddUserAsync(user);
        _waitingRooms.Add(waitingRoom);
        return waitingRoom.ToDto(user.Id);
    }

    public async Task<WaitingRoomDto> JoinWaitingRoom(string code, WaitingUser user)
    {
        var roomToJoin = _waitingRooms.First(x => x.ValidateCode(code));
        await roomToJoin.AddUserAsync(user);
        return roomToJoin.ToDto(user.Id);
    }

    public void Leave(Guid userId)
    {
        var roomToLeave = _waitingRooms.FirstOrDefault(x => x.IncludesUser(userId));
        if (roomToLeave == null)
        {
            return;
        }

        roomToLeave!.Leave(userId);
        if (!roomToLeave.HasAnyUser)
        {
            _waitingRooms.Remove(roomToLeave);
        }
    }

    public async Task StartGame(string code)
    {
        var room = _waitingRooms.First(x => x.ValidateCode(code));
        await _gameService.StartGame(room);
        room.ClearCode();
    }
}
