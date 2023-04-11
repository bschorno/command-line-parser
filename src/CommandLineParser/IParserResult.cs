namespace CommandLineParser
{
    public interface IParserResult
    {
        bool HasErrors { get; }

        IEnumerable<IParserResultCommand> Commands { get; }
    }
}
