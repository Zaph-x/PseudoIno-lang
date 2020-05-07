namespace AbstractSyntaxTree.Objects.Nodes
{
    public interface IExpr : ITyped
    {
        ITerm LeftHand { get; set; }
        OperatorNode Operator { get; set; }
        IExpr RightHand { get; set; }

        void Accept(Visitor visitor);
    }
}