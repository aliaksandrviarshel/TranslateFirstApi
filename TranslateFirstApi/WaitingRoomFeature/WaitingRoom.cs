using TranslateFirstApi.GameFeature;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.TranslateFirstHubFeature.HubSender;
using TranslateFirstApi.WaitingRoomFeature.CodeGeneration;
using TranslateFirstApi.WaitingRoomFeature.Counting;
using TranslateFirstApi.WaitingRoomFeature.Waiter;

namespace TranslateFirstApi.WaitingRoomFeature;

public class WaitingRoom
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private readonly ICodeGenerator _codeGenerator;
    private readonly IRoomHubSender _waitingRoomHubSender;
    private List<WaitingUser> _users = new();
    private string? _code;

    public WaitingRoom(ICodeGenerator codeGenerator, IRoomHubSender waitingRoomHubSender)
    {
        _codeGenerator = codeGenerator;
        _waitingRoomHubSender = waitingRoomHubSender;
    }

    public async Task<WaitingRoomDto> AddUserAsync(WaitingUser user)
    {
        await user.CheckAndAssignNicknameAsync();
        if (!HasAnyUser)
        {
            _code = _codeGenerator.Generate();
        }

        _users.Add(user);
        _waitingRoomHubSender.AddUser(user.Id);
        _waitingRoomHubSender.ToAllExceptAsync(user.Id, "UserJoined", user.ToDto());
        return new WaitingRoomDto(_code!, new(), user.ToDto());
    }

    public void Leave(Guid id)
    {
        _users = _users.Where(x => x.Id != id).ToList();
        _waitingRoomHubSender.RemoveUser(id);
        if (!HasAnyUser)
        {
            _code = null;
            return;
        }
        _waitingRoomHubSender.ToAllAsync("WaitingRoomUserLeft", id);
    }

    public Game StartGame()
    {
        var counter = new Counter();
        return new Game(_users.Select(x => x.ToGamePartipiant(counter.Next())).ToList(), _waitingRoomHubSender);
    }

    public bool HasAnyUser => _users.Any();

    public bool IncludesUser(Guid userId) => _users.Any(x => x.Id == userId);

    public bool ValidateCode(string code) => _code == code;

    public void ClearCode() => _code = null;

    public WaitingRoomDto ToDto(Guid meId)
    {
        return new WaitingRoomDto(_code!, _users.Where(x => x.Id != meId).Select(x => x.ToDto()).ToList(), _users.First(x => x.Id == meId).ToDto());
    }
}
