using CmdLineParser;

namespace CmdLineParser.Test.Unit
{
    public class Options
    {
        private class Parameters
        {
            public string? Path { get; set; }

            public int Port { get; set; }
        }

        private IParser _parser;

        public Options()
        {
            var parserBuilder = CommandLineParser.CreateBuilder();
            parserBuilder.AddCommand<Parameters>("run", _ =>
            {
                _.AddOption('f', "file", parameter => parameter.Path);
                _.AddOption('p', "port", parameter => parameter.Port).Default(5050);
            });
            _parser = parserBuilder.Build();
        }

        [Fact]
        public void Test1()
        {
            var result = _parser.Parse(new string[] { "run", "--file=text.txt", "--port=1000" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            var parameters = firstCommand.GetObject<Parameters>();

            Assert.NotNull(parameters);
            Assert.Equal("text.txt", parameters.Path);
            Assert.Equal(1000, parameters.Port);
        }

        [Fact]
        public void Test2()
        {
            var result = _parser.Parse(new string[] { "run", "-f=text.txt", "-p=1000" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            var parameters = firstCommand.GetObject<Parameters>();

            Assert.NotNull(parameters);
            Assert.Equal("text.txt", parameters.Path);
            Assert.Equal(1000, parameters.Port);
        }

        [Fact]
        public void Test3()
        {
            var result = _parser.Parse(new string[] { "run", "-f=text.txt" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            var parameters = firstCommand.GetObject<Parameters>();

            Assert.NotNull(parameters);
            Assert.Equal("text.txt", parameters.Path);
            Assert.Equal(5050, parameters.Port);
        }

        [Fact]
        public void Test4()
        {
            var result = _parser.Parse(new string[] { "run" });

            var firstCommand = result.Commands.FirstOrDefault();

            Assert.NotNull(firstCommand);
            var parameters = firstCommand.GetObject<Parameters>();

            Assert.NotNull(parameters);
            Assert.Null(parameters.Path);
            Assert.Equal(5050, parameters.Port);
        }
    }
}
