using System.Collections.Generic;
namespace Parser.Objects
{
    public class AstNode
    {
        public AstNode Parent { get; set; }
        public ParseToken Token { get; set; } // TODO Dette burde være den nuværende token
        public List<AstNode> Children { get; set; } = new List<AstNode>();

        public AstNode(AstNode parent = null, object token)
        {
            this.Parent = parent;
            this.Token = token;
        }

        public void AddChild([NotNull]AstNode child)
        {
            Children.Add(child);
        }

        public void TryReduceChildren(List<AstNode> nodes)
        {

        }
    }
}