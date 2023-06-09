﻿namespace CmdLineParser.Internals
{
    internal interface IParserCommand
    {
        string CommandName { get; }

        IReadOnlyList<IParserCommand> Commands { get; }

        IReadOnlyList<IParserArgument> Arguments { get; }

        IReadOnlyList<IParserOption> Options { get; }

        ParserResultCommand CreateParserResultCommand(ParserResult parserResult);
    }
}
