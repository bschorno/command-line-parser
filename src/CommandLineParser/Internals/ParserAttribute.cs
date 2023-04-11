using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineParser.Internals
{
    internal abstract class ParserAttribute : IParserAttribute
    {
        protected bool _required;

        public bool IsRequired => _required;

        protected ParserAttribute() 
        { 
            
        }

        public abstract void ParseValue(IParserResultCommand parentResultCommand, string? value);
    }

    internal abstract class ParserAttribute<TCommand, TAttribute> : ParserAttribute, IParserAttribute<TAttribute>, IParserAttribute
    {
        private readonly Expression<Func<TCommand, TAttribute>> _propertySelector;

        private TAttribute? _defaultValue;

        public ParserAttribute(Expression<Func<TCommand, TAttribute>> propertySelector)
            : base()
        {
            _propertySelector = propertySelector;
        }

        public IParserAttribute<TAttribute> Required(bool required = true)
        {
            _required = required;
            return this;
        }

        public IParserAttribute<TAttribute> Default(TAttribute defaultValue)
        {
            _defaultValue = defaultValue;
            return this;
        }

        public override void ParseValue(IParserResultCommand parentResultCommand, string? value)
        {
            var objectReference = parentResultCommand.GetObject<TCommand>();
            if (objectReference == null)
                return;

            var propertyInfo = (PropertyInfo)((MemberExpression)_propertySelector.Body).Member;
        }
    }
}
