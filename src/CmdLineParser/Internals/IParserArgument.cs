namespace CmdLineParser.Internals
{
    internal interface IParserArgument : IParserAttribute
    {
        string Name { get; }
    }
}
