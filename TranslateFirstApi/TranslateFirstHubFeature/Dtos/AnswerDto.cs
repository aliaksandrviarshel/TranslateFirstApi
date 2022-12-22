namespace TranslateFirstApi.TranslateFirstHubFeature.Dtos;

public class AnswerDto
{
    public AnswerDto(Guid userId, Guid translateId)
    {
        UserId = userId;
        TranslateId = translateId;
    }

    public Guid UserId { get; set; }
    public Guid TranslateId { get; set; }
}
