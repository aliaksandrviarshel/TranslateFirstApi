using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using TranslateFirstApi.GameFeature.Services;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.TranslateFirstHubFeature.Services;
using TranslateFirstApi.WaitingRoomFeature.Services;
using TranslateFirstApi.WaitingRoomFeature.Waiter;
using TranslateFirstApi.WaitingRoomFeature.Waiter.NicknameGeneration;

namespace TranslateFirstApi.TranslateFirstHubFeature;

public class TranslateFirstHub : Hub
{
    private readonly IWaitingRoomService _waitingRoomService;
    private readonly IUsersConnectionsService _usersConnectionsService;
    private readonly IGameService _gameService;

    public override async Task OnConnectedAsync()
    {
        _usersConnectionsService.Add(Guid.NewGuid(), Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _usersConnectionsService.Remove(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public TranslateFirstHub(IWaitingRoomService waitingRoomService, IUsersConnectionsService usersConnectionsService, IGameService gameService)
    {
        _waitingRoomService = waitingRoomService;
        _usersConnectionsService = usersConnectionsService;
        _gameService = gameService;
    }

    public async Task<WaitingRoomDto> CreateWaitingRoom(string nickname)
    {
        var userId = _usersConnectionsService.GetUserId(Context.ConnectionId);
        var user = new WaitingUser(userId, nickname);
        return await _waitingRoomService.CreateWaitingRoom(user);
    }

    public async Task<WaitingRoomDto> JoinWaitingRoom(string joinGameDto1)
    {
        var userId = _usersConnectionsService.GetUserId(Context.ConnectionId);
        var joinGameDto = JsonConvert.DeserializeObject<JoinGameDto>(joinGameDto1);

        var user = new WaitingUser(userId, joinGameDto.Nickname);
        return await _waitingRoomService.JoinWaitingRoom(joinGameDto.Code, user);
    }

    public async Task StartGame(string code)
    {        
        await _waitingRoomService.StartGame(code);
    }

    public async Task<GameDto> GetGame(string getGameDto1)
    {
        var getGameDto = JsonConvert.DeserializeObject<GetGameDto>(getGameDto1);
        Guid userId = getGameDto.UserId;
        Guid gameId = getGameDto.GameId;

        return await _gameService.GetGame(userId, gameId);
    }

    public async Task<UserAnsweredDto> Answer(string answerDto1)
    {
        var answerDto = JsonConvert.DeserializeObject<AnswerDto>(answerDto1);

        return await _gameService.Answer(answerDto.UserId, answerDto.TranslateId);
    }

    public async Task Leave(string userId1)
    {
        Guid userId = Guid.Parse(userId1);

        _waitingRoomService.Leave(userId);
        await _gameService.Leave(userId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());
    }
}
