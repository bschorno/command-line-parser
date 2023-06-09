﻿namespace CmdLineParser.Internals
{
    internal interface IParserOption : IParserAttribute
    {
        char ShortName { get; }

        string LongName { get; }
    }
}
