using TranslateFirstApi.GameFeature.WordOption;
using TranslateFirstApi.TranslateFirstHubFeature.Dtos;

namespace TranslateFirstApi.GameFeature.Gamer;
public interface IGamer
{
    Guid Id { get; }
    int CorrectAnswersCount { get; }

    void SetPosition(int value);
    bool Answer(Guid translateId);
    void SetCurrentWord(Word word);
    UserRatingDto ToDto();
}