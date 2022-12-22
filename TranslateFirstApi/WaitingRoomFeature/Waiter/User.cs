using TranslateFirstApi.GameFeature.Gamer;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;
using TranslateFirstApi.WaitingRoomFeature.Waiter.NicknameGeneration;

namespace TranslateFirstApi.WaitingRoomFeature.Waiter;

public class WaitingUser
{
    public WaitingUser(Guid id, string nickame)
    {
        Id = id;
        Nickname = nickame;
        _nicknameGenerator = new NicknameGenerator();
    }

    private INicknameGenerator _nicknameGenerator;
    public Guid Id { get; set; }
    public string Nickname { get; set; }

    public async Task CheckAndAssignNicknameAsync()
    {
        if (!string.IsNullOrEmpty(Nickname))
        {
            return;
        }

        Nickname = await _nicknameGenerator.Generate();
    }

    public UserDto ToDto() => new(Id, Nickname);

    public GamerManager ToGamePartipiantManager(int ratingPosition)
    {
        return new GamerManager(Id, Nickname, ratingPosition);
    }
}
