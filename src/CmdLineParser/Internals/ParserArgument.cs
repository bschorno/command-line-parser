using CmdLineParser;
using System.Linq.Expressions;

namespace CmdLineParser.Internals
{
    internal class ParserArgument<TCommand, TAttribute> : ParserAttribute<TCommand, TAttribute>, IParserArgument<TAttribute>, IParserArgument where TCommand : new()
    {
        private readonly string _name;

        public string Name => _name;

        public ParserArgument(string name, Expression<Func<TCommand, TAttribute>> propertySelector)
            : base(propertySelector)
        {
            _name = name;
        }
    }
}
