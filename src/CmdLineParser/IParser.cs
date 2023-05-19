namespace CmdLineParser
{
    public interface IParser
    {
        IParserResult Parse(string[] args);
    }
}
