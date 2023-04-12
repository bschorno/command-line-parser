namespace CommandLineParser
{
    public interface IParserResult
    {
        IEnumerable<IParserResultCommand> Commands { get; }
    }
}
