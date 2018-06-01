using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Extentions;

namespace Logic.MapModel
{
    class MapBuilder
    {
        public static MapNode BuildMap(Color[,] board)
        {
            //Used as lookup for with node a point belongs to.
            MapNode[,] lookup = BuildLookupGrid(board);

            AddMapNeighbors(lookup);

            return lookup.GetAt(0, 0);
        }

        class MapTreeKeyValuePair
        {
            public MapNode MapNode { get; set; }
            public TreeNode TreeNode { get; set; }
        }
        public static TreeNode BuildTree(Color[,] board)
        {
            MapNode head = BuildMap(board);
            TreeNode root = new TreeNode(null, head.Color);

            Queue<MapTreeKeyValuePair> frontLine = new Queue<MapTreeKeyValuePair>();
            ISet<MapNode> visited = new HashSet<MapNode>();
            frontLine.Enqueue(new MapTreeKeyValuePair{MapNode = head, TreeNode = root});
            visited.Add(head);

            while (frontLine.Count > 0)
            {
                MapTreeKeyValuePair mapTree = frontLine.Dequeue();
                foreach (MapNode neighbor in mapTree.MapNode.GetNeighbors())
                {
                    if(!visited.Contains(neighbor))
                    {
                        TreeNode childTreeNode = new TreeNode(mapTree.TreeNode, neighbor.Color);
                        //Claim this map node as your child
                        mapTree.TreeNode.AddChildern(childTreeNode);
                        //mark map node as visited, no one can claim this map node again
                        visited.Add(neighbor);
                        //queue it up to find it's children
                        frontLine.Enqueue(new MapTreeKeyValuePair { MapNode = neighbor, TreeNode = childTreeNode });
                    }
                }
            }

            return root;
        }

        //private static TreeNode[,] ToTreeNode(MapNode[,] lookup)
        //{
        //    TreeNode[,] treeLookup = new TreeNode[lookup.Height(), lookup.Width()];
        //    for (int y = 0; y < lookup.Height(); y++)
        //    {
        //        for (int x = 0; x < lookup.Width(); x++)
        //        {
        //            TreeNode treeNode = new TreeNode(lookup.GetAt(x, y).Color);
        //            treeLookup.SetAt(x, y, treeNode);
        //            throw new NotImplementedException("Doesn't keep the same reference for touching colors");
        //        }
        //    }
        //    return treeLookup;
        //}

        private static MapNode[,] BuildLookupGrid(Color[,] board)
        {
            MapNode[,] lookup = new MapNode[board.Height(), board.Width()];

            //build lookup
            for (int y = 0; y < board.Height(); y++)
            {
                for (int x = 0; x < board.Width(); x++)
                {
                    //Create MapNode
                    bool isLeftSame = board.CanGetLeft(x) && board.GetAt(x, y) == board.GetLeftOf(x, y);
                    bool isAboveSame = board.CanGetAbove(y) && board.GetAt(x, y) == board.GetAboveOf(x, y);

                    if (isLeftSame && isAboveSame) //check above & to the left
                    {
                        MapNode left = lookup.GetLeftOf(x, y);
                        MapNode above = lookup.GetAboveOf(x, y);
                        lookup.SetAt(x, y, left);
                        if (left != above)
                        {
                            left.Merge(above);
                            //update "above's" references to left
                            for (int updateY = y; updateY >= 0; updateY--)
                            {
                                for (int updateX = x; updateX >= 0; updateX--)
                                {
                                    if (lookup.GetAt(updateX, updateY) == above)
                                        lookup.SetAt(updateX, updateY, left);
                                }
                            }
                        }
                    }
                    else if (isLeftSame) // check to the left
                    {
                        lookup.SetAt(x, y, lookup.GetLeftOf(x, y));
                    }
                    else if (isAboveSame) // check above
                    {
                        lookup.SetAt(x, y, lookup.GetAboveOf(x, y));
                    }
                    else
                    {
                        lookup.SetAt(x, y, new MapNode(board.GetAt(x, y)));
                    }
                }
            }
            return lookup;
        }

        private static void AddMapNeighbors(MapNode[,] lookup)
        {
            //Add neighbors
            for (int y = 0; y < lookup.Height(); y++)
            {
                for (int x = 0; x < lookup.Width(); x++)
                {
                    bool isLeftNotSame = lookup.CanGetLeft(x) && lookup.GetAt(x, y).Color != lookup.GetLeftOf(x, y).Color;
                    bool isAboveNotSame = lookup.CanGetAbove(y) && lookup.GetAt(x, y).Color != lookup.GetAboveOf(x, y).Color;
                    bool isRightNotSame = lookup.CanGetRight(x) && lookup.GetAt(x, y).Color != lookup.GetRightOf(x, y).Color;
                    bool isBelowNotSame = lookup.CanGetBelow(y) && lookup.GetAt(x, y).Color != lookup.GetBelowOf(x, y).Color;
                    MapNode currentNode = lookup.GetAt(x, y);
                    if (isAboveNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetAboveOf(x, y));
                    }
                    if (isLeftNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetLeftOf(x, y));
                    }
                    if (isRightNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetRightOf(x, y));
                    }
                    if (isBelowNotSame)
                    {
                        currentNode.AddNeighbor(lookup.GetBelowOf(x, y));
                    }
                }
            }
        }


        //private static MapNode[,] AddTreeChildren(MapNode[,] lookup)
        //{
        //    TreeNode[,] parents = new TreeNode[lookup.Height(), lookup.Width()];
        //    TreeNode root = new TreeNode(lookup[0, 0].Color);
        //    //If it has a parent, it's been claimed by a parent node already
        //    bool[,] visited = new bool[lookup.Height(), lookup.Width()];
        //    Queue<TreeNode> frontLine = new Queue<TreeNode>();
        //    frontLine.Enqueue(new TreeNode());

        //    while (frontLine.Count > 0)
        //    {
        //        TreeNode current = frontLine.Dequeue();


        //    }

        //    return parents;
        //}

    }
}
