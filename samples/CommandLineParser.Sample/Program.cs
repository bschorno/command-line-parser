using CommandLineParser;

namespace CommandLineParser.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parserBuilder = CommandLineParser.CreateBuilder();

            parserBuilder.AddCommand<ConfigParameters>("config", _ =>
            {
                _.AddArgument(attr => attr.Path).Required();

                _.AddCommand<ConfigAddParameters>("add", command => command.AddParameters!, addCommand =>
                {
                    addCommand.AddOption('o', "overwrite", attr => attr.Overwrite);
                }).Callback(parameters =>
                {
                    Console.WriteLine("Command 'add' was selected");
                });

                _.AddCommand<ConfigRemoveParameters>("remove", command => command.RemoveParameters!, removeCommand =>
                {
                    removeCommand.AddOption('a', "all-instances", attr => attr.AllInstances);
                    removeCommand.AddOption('n', "no-database-update", attr => attr.NoDatabaseUpdate);
                }).Callback(parameters =>
                {
                    Console.WriteLine("Command 'remove' was selected");
                });
            });

            var parser = parserBuilder.Build();
            var result = parser.Parse(args);
        }

        public class ConfigParameters
        {
            public string? Path { get; set; }

            public ConfigAddParameters? AddParameters { get; set; }

            public ConfigRemoveParameters? RemoveParameters { get; set; }
        }

        public class ConfigAddParameters
        {
            public bool Overwrite { get; set; }
        }

        public class ConfigRemoveParameters
        {
            public bool AllInstances { get; set; }

            public bool NoDatabaseUpdate { get; set; }
        }
    }
}