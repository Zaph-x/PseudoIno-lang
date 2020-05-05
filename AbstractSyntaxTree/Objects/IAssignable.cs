namespace AbstractSyntaxTree.Objects
{
    public interface IAssginable
    {
        string Id { get; set; }
        public void Accept(Visitor visitor);
    }

}