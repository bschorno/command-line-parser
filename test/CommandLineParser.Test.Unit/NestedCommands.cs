namespace CommandLineParser.Test.Unit
{
    public class NestedCommands
    {
        private class ConfigParameters
        {
            public string? Path { get; set; }
            public ConfigAddParameters? AddParameters { get; set; }
            public ConfigRemoveParameters? RemoveParameters { get; set; }
        }
        
        private class ConfigAddParameters
        {
            public bool Overwrite { get; set; }
        }

        private class ConfigRemoveParameters
        {
            public bool AllInstances { get; set; }
            public bool NoDatabaseUpdate { get; set; }
        }

        private IParser _parser;

        public NestedCommands()
        {
            var parserBuilder = CommandLineParser.CreateBuilder();
            parserBuilder.AddCommand<ConfigParameters>("config", _ =>
            {
                _.AddArgument(attr => attr.Path).Required();
                _.AddCommand<ConfigAddParameters>("add", command => command.AddParameters!, addCommand =>
                {
                    addCommand.AddOption('o', "overwrite", attr => attr.Overwrite);
                });
                _.AddCommand<ConfigRemoveParameters>("remove", command => command.RemoveParameters!, removeCommand =>
                {
                    removeCommand.AddOption('a', "all-instances", attr => attr.AllInstances);
                    removeCommand.AddOption('n', "no-database-update", attr => attr.NoDatabaseUpdate);
                });
            });
            _parser = parserBuilder.Build();
        }

        [Fact]
        public void Test1()
        {
            var result = _parser.Parse(new string[] { "config" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            Assert.Equal("config", firstCommand.Command);
        }

        [Fact]
        public void Test2()
        {
            var result = _parser.Parse(new string[] { "config", "input.txt" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            Assert.Equal("config", firstCommand.Command);

            var configParameters = firstCommand.GetObject<ConfigParameters>();

            Assert.NotNull(configParameters);
            Assert.Equal("input.txt", configParameters.Path);
        }

        [Fact]
        public void Test3()
        {
            var result = _parser.Parse(new string[] { "config", "add", "-o" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            Assert.Equal("config", firstCommand.Command);

            var secondCommand = result.Commands.Last();

            Assert.NotNull(secondCommand);
            Assert.Equal("add", secondCommand.Command);

            var configAddParameters = secondCommand.GetObject<ConfigAddParameters>();

            Assert.NotNull(configAddParameters);
            Assert.True(configAddParameters.Overwrite);
        }

        [Fact]
        public void Test4()
        {
            var result = _parser.Parse(new string[] { "config", "add", "--overwrite" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            Assert.Equal("config", firstCommand.Command);

            var secondCommand = result.Commands.Last();

            Assert.NotNull(secondCommand);
            Assert.Equal("add", secondCommand.Command);

            var configAddParameters = secondCommand.GetObject<ConfigAddParameters>();

            Assert.NotNull(configAddParameters);
            Assert.True(configAddParameters.Overwrite);
        }

        [Fact]
        public void Test5()
        {
            var result = _parser.Parse(new string[] { "config", "remove", "-an" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            Assert.Equal("config", firstCommand.Command);

            var secondCommand = result.Commands.Last();

            Assert.NotNull(secondCommand);
            Assert.Equal("remove", secondCommand.Command);

            var configRemoveParameters = secondCommand.GetObject<ConfigRemoveParameters>();

            Assert.NotNull(configRemoveParameters);
            Assert.True(configRemoveParameters.AllInstances);
            Assert.True(configRemoveParameters.NoDatabaseUpdate);
        }

        [Fact]
        public void Test6()
        {
            var result = _parser.Parse(new string[] { "config", "remove", "--all-instances", "--no-database-update" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            Assert.Equal("config", firstCommand.Command);

            var secondCommand = result.Commands.Last();

            Assert.NotNull(secondCommand);
            Assert.Equal("remove", secondCommand.Command);

            var configRemoveParameters = secondCommand.GetObject<ConfigRemoveParameters>();

            Assert.NotNull(configRemoveParameters);
            Assert.True(configRemoveParameters.AllInstances);
            Assert.True(configRemoveParameters.NoDatabaseUpdate);
        }
    }
}