using System;
using Lexer.Exceptions;

namespace Lexer.Objects
{
    public class TypeContext
    {
        private TokenType _tokenType { get; set; }
        public TokenType Type { get => _tokenType; }
        private bool _float {get;set;}
        public bool IsFloat {
            get => _float;
            set {
                if (Type == TokenType.NUMERIC)
                {
                    this._float = value;
                } else {
                    throw new InvalidOperationException($"Type {Type} is assigned an invalid float bool.");
                }
            }
        }
        public TypeContext(TokenType type)
        {
            ValidateType(type);
        }

        private void ValidateType(TokenType type)
        {
            switch (type)
            {
                case TokenType.NUMERIC:
                case TokenType.OP_AND:
                case TokenType.OP_OR:
                case TokenType.OP_GREATER:
                case TokenType.OP_LESS:
                case TokenType.OP_GEQ:
                case TokenType.OP_LEQ:
                case TokenType.OP_PLUS:
                case TokenType.OP_MINUS:
                case TokenType.OP_TIMES:
                case TokenType.OP_DIVIDE:
                case TokenType.OP_MODULO:
                case TokenType.OP_NOT:
                case TokenType.OP_EQUAL:
                case TokenType.BOOL:
                case TokenType.STRING:
                case TokenType.APIN:
                case TokenType.DPIN:
                case TokenType.ARR:
                case TokenType.FUNC:
                case TokenType.VAR:
                    _tokenType = type;
                    break;
                default:
                    throw new InvalidTypeException($"Type {type} is invalid.");
            }
        }
        public override string ToString() => ""+this.Type;
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TypeContext))
                return this.Type == ((TypeContext)obj).Type;
            else return false;
        }
    }
}