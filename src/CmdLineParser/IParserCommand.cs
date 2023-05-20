using System.Linq.Expressions;

namespace CmdLineParser
{
    public interface IParserCommand<T1> where T1 : new()
    {
        IParserCommand<T1> Callback(Action<T1> callback);

        IParserCommand<T1> AddCommand(string commandName);

        IParserCommand<T1> AddCommand(string commandName, Action<IParserCommand<T1>>? buildingAction);

        IParserCommand<T1, K> AddCommand<K>(string commandName, Expression<Func<T1, K>> propertySelector) where K : new();

        IParserCommand<T1, K> AddCommand<K>(string commandName, Expression<Func<T1, K>> propertySelector, Action<IParserCommand<T1, K>>? buildingAction) where K : new();

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T1, K>> propertySelector);

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T1, K>> propertySelector, Action<IParserOption<K>>? buildingAction);

        IParserArgument<K> AddArgument<K>(Expression<Func<T1, K>> propertySelector);

        IParserArgument<K> AddArgument<K>(Expression<Func<T1, K>> propertySelector, Action<IParserArgument<K>>? buildingAction);
    }

    public interface IParserCommand<T1, T2> where T1 : new() where T2 : new()
    {
        IParserCommand<T1, T2> Callback(Action<T1, T2> callback);

        IParserCommand<T1, T2> AddCommand(string command);

        IParserCommand<T1, T2> AddCommand(string command, Action<IParserCommand<T1, T2>>? buildingAction);

        IParserCommand<T1, T2, K> AddCommand<K>(string commandName, Expression<Func<T2, K>> propertySelector) where K : new();

        IParserCommand<T1, T2, K> AddCommand<K>(string commandName, Expression<Func<T2, K>> propertySelector, Action<IParserCommand<T1, T2, K>>? buildingAction) where K : new();

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T2, K>> propertySelector);

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T2, K>> propertySelector, Action<IParserOption<K>>? buildingAction);

        IParserArgument<K> AddArgument<K>(Expression<Func<T2, K>> propertySelector);

        IParserArgument<K> AddArgument<K>(Expression<Func<T2, K>> propertySelector, Action<IParserArgument<K>>? buildingAction);
    }

    public interface IParserCommand<T1, T2, T3> where T1 : new() where T2 : new() where T3 : new()
    {
        IParserCommand<T1, T2, T3> Callback(Action<T1, T2, T3> callback);

        IParserCommand<T1, T2, T3> AddCommand(string command);

        IParserCommand<T1, T2, T3> AddCommand(string command, Action<IParserCommand<T1, T2, T3>>? buildingAction);

        IParserCommand<T1, T2, T3, K> AddCommand<K>(string commandName, Expression<Func<T3, K>> propertySelector) where K : new();

        IParserCommand<T1, T2, T3, K> AddCommand<K>(string commandName, Expression<Func<T3, K>> propertySelector, Action<IParserCommand<T1, T2, T3, K>>? buildingAction) where K : new();

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T3, K>> propertySelector);

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T3, K>> propertySelector, Action<IParserOption<K>>? buildingAction);

        IParserArgument<K> AddArgument<K>(Expression<Func<T3, K>> propertySelector);

        IParserArgument<K> AddArgument<K>(Expression<Func<T3, K>> propertySelector, Action<IParserArgument<K>>? buildingAction);
    }

    public interface IParserCommand<T1, T2, T3, T4> where T1 : new() where T2 : new() where T3 : new() where T4 : new()
    {
        IParserCommand<T1, T2, T3, T4> Callback(Action<T1, T2, T3, T4> callback);

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T4, K>> propertySelector);

        IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T4, K>> propertySelector, Action<IParserOption<K>>? buildingAction);

        IParserArgument<K> AddArgument<K>(Expression<Func<T4, K>> propertySelector);

        IParserArgument<K> AddArgument<K>(Expression<Func<T4, K>> propertySelector, Action<IParserArgument<K>>? buildingAction);
    }
}
