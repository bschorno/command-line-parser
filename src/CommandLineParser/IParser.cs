namespace CommandLineParser
{
    public interface IParser
    {
        IParserResult Parse(string[] args);
    }
}
