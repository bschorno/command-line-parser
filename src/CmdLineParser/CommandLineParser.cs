using CmdLineParser.Internals;

namespace CmdLineParser
{
    public static class CommandLineParser
    {
        public static IParserBuilder CreateBuilder()
        {
            return new ParserBuilder();
        }

        public static IParserBuilder CreateBuilder(Action<IParserBuilder> buildingAction)
        {
            var builder = new ParserBuilder();
            buildingAction.Invoke(builder);
            return builder;
        }
    }
}
