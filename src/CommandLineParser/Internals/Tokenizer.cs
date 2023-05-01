namespace CommandLineParser.Internals
{
    internal class Tokenizer
    {
        private readonly ParserBuilder _parserBuilder;
        private IParserCommand? _currentCommand = null;
        private int _currentCommandArgumentIndex = 0;
        
        private readonly string[] _arguments;
        private int _position;

        private List<Token> _tokenList = new List<Token>();
        private TokenType? _requiredType = TokenType.Command;

        public Tokenizer(string[] arguments, ParserBuilder parserBuilder)
        {
            _arguments = arguments;
            _parserBuilder = parserBuilder;

            Scan();
        }

        public Token? GetNextToken()
        {
            if (_tokenList.Count > 0)
            {
                Token token = _tokenList[0];
                _tokenList.RemoveAt(0);
                return token;
            }
            return null;
        }

        public Token? PeekNextToken()
        {
            if (_tokenList.Count > 0)
                return _tokenList[0];
            return null;
        }

        private void Scan()
        {
            while (Peek() != null)
            {
                if (_currentCommand == null)
                    _requiredType = TokenType.Command;

                Tokenize(Next()!);
            }
        }

        private void Tokenize(ReadOnlySpan<char> arg)
        {
            if (_requiredType != null)
            {
                switch (_requiredType)
                {
                    case TokenType.Command:
                        TokenizeCommand(arg);
                        return;
                    case TokenType.Value:
                        TokenizeValue(arg);
                        return;
                }
            }

            if (_currentCommand == null)
                return;

            if (arg.StartsWith("--", StringComparison.OrdinalIgnoreCase))
            {
                TokenizeLongOption(arg[2..]);
            }
            else if (arg.StartsWith("-", StringComparison.OrdinalIgnoreCase))
            {
                TokenizeShortOption(arg[1..]);
            }
            else if (arg.StartsWith("=", StringComparison.OrdinalIgnoreCase))
            {
                TokenizeValue(arg[1..]);
            }
            else if (arg.StartsWith("\"", StringComparison.CurrentCultureIgnoreCase))
            {
                TokenizeArgument(arg);
            }
            else
            {
                TokenizeCommand(arg);
                Token lastAddedToken = _tokenList.Last();
                if (lastAddedToken.Type == TokenType.NotDefined)
                {
                    if (_currentCommand!.Arguments.Count > _currentCommandArgumentIndex)
                    {
                        _tokenList.RemoveAt(_tokenList.Count - 1);
                        TokenizeArgument(arg);
                    }
                }
            }
        }

        private void TokenizeCommand(ReadOnlySpan<char> arg)
        {
            var argTrimmed = arg.Trim();
            foreach (var command in _currentCommand?.Commands ?? _parserBuilder.Commands)
            {
                if (argTrimmed.Equals(command.CommandName, StringComparison.OrdinalIgnoreCase))
                {
                    _currentCommand = command;
                    _tokenList.Add(new TokenCommand(_currentCommand, argTrimmed.ToString()));
                    _requiredType = null;
                    return;
                }
            }
            _tokenList.Add(new Token(TokenType.NotDefined, argTrimmed.ToString()));
        }

        private void TokenizeLongOption(ReadOnlySpan<char> arg)
        {
            var equalsIndex = arg.IndexOf('=');
            var argTrimmed = arg.Trim();
            if (equalsIndex > 0)
                argTrimmed = argTrimmed[..equalsIndex];

            bool found = false;
            foreach (var option in _currentCommand!.Options)
            {
                if (argTrimmed.Equals(option.LongName, StringComparison.OrdinalIgnoreCase))
                {
                    _tokenList.Add(new TokenOption(option, argTrimmed.ToString()));
                    if (option.IsRequired)
                    {
                        _requiredType = TokenType.Value;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
                _tokenList.Add(new Token(TokenType.NotDefined, argTrimmed.ToString()));

            if (++equalsIndex > argTrimmed.Length)
            {
                _requiredType = TokenType.Value;
                Tokenize(arg[equalsIndex..]);
            }
        }

        private void TokenizeShortOption(ReadOnlySpan<char> arg)
        {
            var argTrimmed = arg.Trim();

            bool found = false;
            foreach (var option in _currentCommand!.Options)
            {
                if (argTrimmed[0].Equals(option.ShortName))
                {
                    _tokenList.Add(new TokenOption(option, argTrimmed[0]));
                    if (option.IsRequired)
                    {
                        _requiredType = TokenType.Value;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
                _tokenList.Add(new Token(TokenType.NotDefined, argTrimmed.ToString()));

            if (argTrimmed.Length > 1)
            {
                if (_requiredType == TokenType.Value)
                    Tokenize(argTrimmed[1..]);
                else if (char.IsLetterOrDigit(argTrimmed[1]))
                    TokenizeShortOption(argTrimmed[1..]);
                else
                    Tokenize(argTrimmed[1..]);
            }   
        }

        private void TokenizeValue(ReadOnlySpan<char> arg)
        {
            var argTrimmed = arg.Trim();
            if (argTrimmed.StartsWith("="))
                argTrimmed = argTrimmed[1..];

            if (argTrimmed.Length == 0)
            {
                _requiredType = TokenType.Value;
                return;
            }

            _tokenList.Add(new TokenValue(argTrimmed.ToString()));
            _requiredType = null;
        }

        private void TokenizeArgument(ReadOnlySpan<char> arg)
        {
            var argTrimmed = arg.Trim();

            _tokenList.Add(new TokenArgument(_currentCommand!.Arguments[_currentCommandArgumentIndex], arg.ToString()));
            _currentCommandArgumentIndex++;
        }

        private string? Peek()
        {
            if (_position >= _arguments.Length)
            {
                return null;
            }
            return _arguments[_position];
        }

        private string? Next()
        {
            var current = Peek();
            _position++;
            return current;
        }
    }
}
