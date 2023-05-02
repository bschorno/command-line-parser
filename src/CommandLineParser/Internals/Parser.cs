namespace CommandLineParser.Internals
{
    internal class Parser : IParser
    {
        private readonly ParserBuilder _parserBuilder;

        public Parser(ParserBuilder parserBuilder)
        {
            _parserBuilder = parserBuilder;
        }

        public IParserResult Parse(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer(args, _parserBuilder);
            ParserResult result = new ParserResult();

            Token? token;
            while ((token = tokenizer.GetNextToken()) != null)
            {
                switch (token.Type)
                {
                    case TokenType.NotDefined:
                        throw new NotDefinedTokenException(token);

                    case TokenType.Command:
                        result.AddCommand(((TokenCommand)token).Command);
                        break;

                    case TokenType.Argument:
                        result.AddAttribute(((TokenArgument)token).Argument, token.Value);
                        break;

                    case TokenType.ShortOption:
                    case TokenType.LongOption:
                        var nextToken = tokenizer.PeekNextToken();
                        if (nextToken != null && nextToken is TokenValue)
                            result.AddAttribute(((TokenOption)token).Option, tokenizer.GetNextToken()!.Value);
                        else
                            result.AddAttribute(((TokenOption)token).Option, null);
                        break;

                    default:
                        break;
                }
            }

            foreach (var command in result.Commands)
            {
                var parserResultCommand = command as ParserResultCommand;
                parserResultCommand?.InvokeCallback();
            }

            return result;
        }
    }
}
