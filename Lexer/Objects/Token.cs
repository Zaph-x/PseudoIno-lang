

namespace Lexer.Objects
{
    public class Token
    {
        public TokenType Type {get;set;}
        public string Value {get;set;}
        public int Line {get;private set;} 
        public int Offset {get;private set;}

        public Token(TokenType type, string val, int line, int offset)
        {
            this.Type = type;
            this.Value = val;
            this.Line = line;
            this.Offset = offset;
        }

        public Token(TokenType type, int line, int offset)
        {
            this.Type = type;
            this.Value = "";
            this.Line = line;
            this.Offset = offset;
        }

        public override string ToString() => $"({Line}:{Offset}) {Type} => {Value}";
    }
}
