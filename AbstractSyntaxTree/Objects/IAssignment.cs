namespace AbstractSyntaxTree.Objects
{
    public interface IAssignment
    {
        void Accept(Visitor visitor);
    }
}