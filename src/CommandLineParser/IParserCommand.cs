using CommandLineParser.Internals;
using System.Linq.Expressions;

namespace CommandLineParser
{
    public interface IParserCommand<T> where T : new()
    {
        IParserCommand<T> Callback(Action<T> callback);

        IParserCommand<T> AddCommand(string commandName);

        IParserCommand<T> AddCommand(string commandName, Action<IParserCommand<T>>? buildingAction);

        IParserCommand<K> AddCommand<K>(string commandName, Expression<Func<T, K>> propertySelector) where K : new ();

        IParserCommand<K> AddCommand<K>(string commandName, Expression<Func<T, K>> propertySelector, Action<IParserCommand<K>>? buildingAction) where K : new();

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T, K>> propertySelector);

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T, K>> propertySelector, Action<IParserOption<K>>? buildingAction);

        IParserArgument<K> AddArgument<K>(Expression<Func<T, K>> propertySelector);

        IParserArgument<K> AddArgument<K>(Expression<Func<T, K>> propertySelector, Action<IParserArgument<K>>? buildingAction);
    }
}
