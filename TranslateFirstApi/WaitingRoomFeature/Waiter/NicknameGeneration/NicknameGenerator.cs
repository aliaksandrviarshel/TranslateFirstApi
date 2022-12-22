using Newtonsoft.Json;

namespace TranslateFirstApi.WaitingRoomFeature.Waiter.NicknameGeneration;

public class NicknameGenerator : INicknameGenerator
{
    private readonly HttpClient _client = new();

    public async Task<string> Generate()
    {
        var response = await _client.GetAsync("https://names.drycodes.com/1");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var nickname = JsonConvert.DeserializeObject<List<string>>(responseBody).First().Replace('_', ' ');

        return nickname;
    }
}
