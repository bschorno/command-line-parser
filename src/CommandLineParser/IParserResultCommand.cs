namespace CommandLineParser
{
    public interface IParserResultCommand
    {
        string Command { get; }

        T? GetObject<T>();
    }

    public interface IParserResultCommand<TCommand> : IParserResultCommand
    {
        TCommand Object { get; }
    }
}
