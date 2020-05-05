using Lexer.Exceptions;

namespace Lexer.Objects
{
    public class TypeContext
    {
        private TokenType _tokenType {get;set;}
        public TokenType Type {get => _tokenType;}

        public TypeContext(TokenType type) {
            ValidateType(type);
        }

        private void ValidateType(TokenType type) 
        {
            switch (type) {
                case TokenType.NUMERIC:
                case TokenType.BOOL:
                case TokenType.STRING:
                case TokenType.APIN:
                case TokenType.DPIN:
                case TokenType.ARR:
                case TokenType.FUNC:
                    _tokenType = type;
                    break;
                default:
                    throw new InvalidTypeException($"Type {type} is invalid.");
            }
        }
    }
}