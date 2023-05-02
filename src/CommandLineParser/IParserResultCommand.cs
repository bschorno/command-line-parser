namespace CommandLineParser
{
    public interface IParserResultCommand
    {
        string Command { get; }

        T? GetObject<T>() where T : new();
    }

    public interface IParserResultCommand<TCommand> : IParserResultCommand
    {
        TCommand Object { get; }
    }
}
