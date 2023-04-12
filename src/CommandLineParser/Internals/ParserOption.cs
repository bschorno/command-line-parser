using System.Linq.Expressions;

namespace CommandLineParser.Internals
{
    internal class ParserOption<TCommand, TAttribute> : ParserAttribute<TCommand, TAttribute>, IParserOption<TAttribute>, IParserOption
    {
        private readonly char _shortName;
        private readonly string _longName;

        public char ShortName => _shortName;

        public string LongName => _longName;


        public ParserOption(char shortName, string longName, Expression<Func<TCommand, TAttribute>> propertySelector)
            : base(propertySelector)
        {
            _shortName = shortName;
            _longName = longName;

            if (typeof(TAttribute) == typeof(bool))
                GetType().GetMethod(nameof(Default))!.Invoke(this, new object[] { true });
        }
    }
}
