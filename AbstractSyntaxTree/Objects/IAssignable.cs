namespace AbstractSyntaxTree.Objects
{
    public interface IAssginable
    {
        string Id { get; set; }
        void Accept(Visitor visitor);
    }

}