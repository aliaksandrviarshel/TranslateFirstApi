namespace TranslateFirstApi.GameFeature.WordOption.Shuffling;

public class Shuffler
{
    public List<T> Shuffle<T>(List<T> list)
    {
        var random = new Random();
        return list.OrderBy(_ => random.Next()).ToList();
    }
}
