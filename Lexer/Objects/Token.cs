namespace Lexer.Objects
{
    /// <summary>
    /// A class representing a token in the source language.
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        /// The type of the token
        /// </summary>
        /// <value>Set in the constructor</value>
        public TokenType Type {get;set;}
        /// <summary>
        /// The value of the token.
        /// </summary>
        /// <value>Empty string if the value is not significant for the token</value>
        public string Value {get;set;}
        /// <summary>
        /// The line the token is placed on
        /// </summary>
        /// <value>Set in the constructor</value>
        public int Line {get;protected set;}
        /// <summary>
        /// The offset of the value for the token
        /// </summary>
        /// <value>Set in the constructor</value>  
        public int Offset {get;protected set;}

        public TypeContext SymbolicType {get;set;}

        /// <summary>
        /// A function to format tokens, when printed to the screen or in other ways used as a string
        /// </summary>
        /// <returns>A string representation of the token</returns>
        public override string ToString() => $"({Line}:{Offset})".PadRight(8) + $" {Type} => {Value}";

        /// <summary>
        /// Overrides the GetHashCode method and returns the value of the enum
        /// </summary>
        /// <returns>int with value of enum</returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}