using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.MapModel
{
    class MapNode
    {
        public Color Color { get; private set; }
        private ISet<MapNode> _neighbors = new HashSet<MapNode>();

        public MapNode(Color color)
        {
            Color = color;
        }

        public void AddNeighbor(MapNode node)
        {
            _neighbors.Add(node);
        }

        public ISet<MapNode> GetNeighbors()
        {
            return _neighbors;
        }

        public void Merge(MapNode mergingNode)
        {
            this._neighbors = new HashSet<MapNode>(this._neighbors.Union(mergingNode._neighbors)); //take all of mergingNode's neighbors
            //update other nodes of mergingNode's deletion
            foreach (MapNode mergingNodesNeighbors in mergingNode._neighbors)
            {
                mergingNodesNeighbors._neighbors.Remove(mergingNode);
                mergingNodesNeighbors._neighbors.Add(this);
            }
            this._neighbors.Remove(this); //can't have reference to yourself
            this.Color = mergingNode.Color;
            mergingNode._neighbors.Clear(); //@OPTIMIZE We don't have to remove them, it should be understood that the node is invalid
        }
        public void PickColor(Color color)
        {
            List<MapNode> matchingNeighbors = _neighbors.Where(mn => mn.Color == color).ToList();
            foreach (MapNode mapNode in matchingNeighbors)
            {
                this.Merge(mapNode);
            }
        }
        /// <summary>
        /// Copies the Color over to a new MapNode & .Clone()s neighbors
        /// </summary>
        /// <returns>A new MapNode</returns>
        public MapNode Clone()
        {
            Dictionary<MapNode, MapNode> originalToClone = new Dictionary<MapNode, MapNode>();
            Clone(originalToClone);
            return originalToClone[this];
        }

        private void Clone(Dictionary<MapNode, MapNode> originalToClone)
        {
            //Add yourself to lookup
            MapNode thisClone;
            if (!originalToClone.TryGetValue(this, out thisClone)) //Determines if this MapNode has been visited yet
            {
                thisClone = new MapNode(this.Color);
                originalToClone.Add(this, thisClone);
                //Add neighbors to lookup
                foreach (MapNode neighbor in _neighbors)
                {
                    neighbor.Clone(originalToClone); //guarentees the neighbor will be in the lookup
                    MapNode neighborClone = originalToClone[neighbor];
                    if (!thisClone._neighbors.Contains(neighborClone))
                        thisClone._neighbors.Add(neighborClone);
                }

            }
        }

        public override string ToString()
        {
            return Color.ToString() + _neighbors.Count + " (" + GetHashCode() + ")"; ;
        }
    }
}
