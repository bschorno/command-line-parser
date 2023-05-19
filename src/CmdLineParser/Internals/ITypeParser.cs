namespace CmdLineParser.Internals
{
    internal interface ITypeParser
    {

    }

    internal interface ITypeParser<T> : ITypeParser
    {
        T Parse(string value);

        bool CanParse(string value);
    }
}
