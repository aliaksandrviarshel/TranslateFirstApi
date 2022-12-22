namespace TranslateFirstApi.WaitingRoomFeature.CodeGeneration;

public class CodeGenerator : ICodeGenerator
{
    public string Generate()
    {
        return new Random().Next(10000, 99999).ToString();
    }
}
