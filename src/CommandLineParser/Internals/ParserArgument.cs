using System.Linq.Expressions;

namespace CommandLineParser.Internals
{
    internal class ParserArgument<TCommand, TAttribute> : ParserAttribute<TCommand, TAttribute>, IParserArgument<TAttribute>, IParserArgument
    {
        public ParserArgument(Expression<Func<TCommand, TAttribute>> propertySelector) 
            : base(propertySelector)
        {

        }
    }
}
