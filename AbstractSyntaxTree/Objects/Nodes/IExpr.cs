using System;
using Lexer.Objects;

namespace AbstractSyntaxTree.Objects.Nodes
{
    public interface IExpr : ITyped
    {
        ITerm LeftHand { get; set; }
        OperatorNode Operator { get; set; }
        IExpr RightHand { get; set; }

        object Accept(Visitor visitor);
        bool IsType(Type type);
    }
}