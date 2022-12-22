namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class WaitingRoomDto
{
    public WaitingRoomDto(string code, List<UserDto> otherUsers, UserDto me)
    {
        Code = code;
        OtherUsers = otherUsers;
        Me = me;
    }

    public string Code { get; set; }
    public List<UserDto> OtherUsers { get; set; }
    public UserDto Me { get; set; }
}