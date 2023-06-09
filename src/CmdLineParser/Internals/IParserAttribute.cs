﻿namespace CmdLineParser.Internals
{
    internal interface IParserAttribute
    {
        bool IsRequired { get; }

        void ParseValue(IParserResultCommand parentResultCommand, string? value);
    }
}
