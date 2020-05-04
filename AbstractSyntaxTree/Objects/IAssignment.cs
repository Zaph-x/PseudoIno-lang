namespace AbstractSyntaxTree.Objects
{
    public interface IAssignment
    {
        public void Accept(Visitor visitor);
    }
}