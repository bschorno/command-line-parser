using System.Linq.Expressions;

namespace CommandLineParser
{
    public interface IParserBuilder
    {
        IParserCommand<K> AddCommand<K>(string commandName) where K : new();

        IParserCommand<K> AddCommand<K>(string commandName, Action<IParserCommand<K>>? buildingAction) where K : new();

        IParser Build();
    }
}
