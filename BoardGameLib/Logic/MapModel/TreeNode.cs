using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.MapModel
{
    class TreeNode
    {
        public TreeNode Parent { get; private set; }
        public Color Color { get; private set; }
        private ISet<TreeNode> _children = new HashSet<TreeNode>();

        public TreeNode(TreeNode parent, Color color)
        {
            Color = color;
            Parent = parent;
        }

        public IEnumerable<TreeNode> GetChildern()
        {
            return _children.AsEnumerable();
        }

        public void AddChildern(TreeNode node)
        {
            _children.Add(node);
        }

        public TreeNode Clone()
        {
            TreeNode clone = new TreeNode(Parent, Color);
            foreach(TreeNode child in _children)
            {
                clone.AddChildern(child.Clone());
            }
            return clone;
        }

        public override string ToString()
        {
            return Color.ToString() + _children.Count + " ("+GetHashCode()+")";
        }
    }
}
