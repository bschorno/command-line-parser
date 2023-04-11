namespace CommandLineParser.Internals
{
    internal enum TokenType
    {
        NotDefined = 0x00,
        Command = 0x01,
        Argument =0x02,
        ShortOption = 0x04,
        LongOption = 0x08,
        Equals = 0x10,
        Value = 0x20   
    }
}
