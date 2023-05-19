namespace CmdLineParser
{
    public interface IParserResult
    {
        IEnumerable<IParserResultCommand> Commands { get; }
    }
}
