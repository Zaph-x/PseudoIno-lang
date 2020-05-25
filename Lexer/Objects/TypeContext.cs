using System;
using Lexer.Exceptions;

namespace Lexer.Objects
{
    /// <summary>
    /// The type context used in the program to check types in the typechecker
    /// </summary>
    public class TypeContext
    {
        /// <summary>
        /// The tokentype of the
        /// </summary>
        /// <value>The tokentype of the typecontext</value>
        private TokenType _tokenType { get; set; }
        /// <summary>
        /// A getter for the private type property
        /// </summary>
        /// <value>The tokentype of the type context</value>
        public TokenType Type { get => _tokenType; }
        /// <summary>
        /// A boolean value signifying if a numeric is a float
        /// </summary>
        /// <value>True if the numeric is a float. Else false</value>
        private bool _float {get;set;}
        /// <summary>
        /// Getter setter property specifying if a value is a float.
        /// </summary>
        /// <exception cref="InvalidOperationException" >If the type of the type context is not a numeric<exception/>
        /// <value></value>
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
        /// <summary>
        /// The constructor for the typecontext
        /// </summary>
        /// <param name="type">The type of the typecontext</param>
        public TypeContext(TokenType type)
        {
            ValidateType(type);
        }
        /// <summary>
        /// A validator method for the type specified.
        /// </summary>
        /// <param name="type">The type to validate</param>
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
                case TokenType.ARRAYINDEX:
                    _tokenType = type;
                    break;
                default:
                    throw new InvalidTypeException($"Type {type} is invalid.");
            }
        }
        /// <inheritdoc cref="System.Object.ToString()"/>
        public override string ToString() => ""+this.Type;
        /// <inheritdoc cref="System.Object.Equals(Object)"/>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TypeContext))
                return this.Type == ((TypeContext)obj).Type;
            else return false;
        }
    }
}