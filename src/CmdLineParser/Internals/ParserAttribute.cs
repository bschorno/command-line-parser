using System.Linq.Expressions;
using System.Reflection;

namespace CmdLineParser.Internals
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

    internal abstract class ParserAttribute<TCommand, TAttribute> : ParserAttribute, IParserAttribute<TAttribute>, IParserAttribute where TCommand : new()
    {
        private readonly Expression<Func<TCommand, TAttribute>> _propertySelector;
        private readonly ITypeParser<TAttribute> _typeParser;

        private TAttribute? _defaultValue;

        public ParserAttribute(Expression<Func<TCommand, TAttribute>> propertySelector)
            : base()
        {
            _propertySelector = propertySelector;
            _typeParser = TypeParser.GetTypeParser<TAttribute>() ?? throw new Exception("Type not supported");
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

            TAttribute? propertyValue = default;

            if (value == null && _defaultValue != null)
                propertyValue = _defaultValue;
            else if (value != null)
            {
                if (_typeParser.CanParse(value))
                    propertyValue = _typeParser.Parse(value);
                else
                    throw new Exception("Can't parse value");
            }

            if (propertyValue != null)
                propertyInfo.SetValue(objectReference, propertyValue, null);
        }
    }
}
