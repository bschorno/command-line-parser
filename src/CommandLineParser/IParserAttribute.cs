using CommandLineParser.Internals;

namespace CmdLineParser
{
    public interface IParserAttribute<T>
    {
        IParserAttribute<T> Required(bool required = true);

        IParserAttribute<T> Default(T defaultValue);
    }
}
