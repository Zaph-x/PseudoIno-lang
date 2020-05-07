using Lexer.Objects;

namespace AbstractSyntaxTree.Objects
{
    public interface IAssginable : ITyped
    {
        string Id { get; set; }
        void Accept(Visitor visitor);
    }

}