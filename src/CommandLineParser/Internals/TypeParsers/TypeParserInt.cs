using CmdLineParser.Internals;

namespace CmdLineParser.Internals.TypeParsers
{
    internal class TypeParserInt : ITypeParser<int>
    {
        public bool CanParse(string value)
        {
            return int.TryParse(value, out _);
        }

        public int Parse(string value)
        {
            return int.Parse(value);
        }
    }
}
