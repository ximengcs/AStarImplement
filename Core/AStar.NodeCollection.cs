
using System.Collections.Generic;

namespace Simon001.PathFinding
{
    public partial class AStar
    {
        private class NodeCollection
        {
            private IAStarHelper m_Helper;
            private Dictionary<int, AStarNode> m_Nodes;

            public bool Empty
            {
                get
                {
                    return m_Nodes.Count == 0;
                }
            }

            public int Count => m_Nodes.Count;

            public NodeCollection(IAStarHelper helper)
            {
                m_Helper = helper;
                m_Nodes = new Dictionary<int, AStarNode>();
            }

            public AStarNode RemoveMinimum()
            {
                AStarNode minimum = null;
                foreach (var node in m_Nodes.Values)
                {
                    if (minimum == null || minimum.FValue > node.FValue)
                    {
                        minimum = node;
                    }
                }

                if (minimum != null)
                {
                    m_Nodes.Remove(m_Helper.GetUniqueId(minimum.Item));
                }
                return minimum;
            }

            public bool Contains(IAStarItem item)
            {
                int id = m_Helper.GetUniqueId(item);
                return m_Nodes.ContainsKey(id);
            }

            public bool TryGet(IAStarItem item, out AStarNode node)
            {
                int id = m_Helper.GetUniqueId(item);
                return m_Nodes.TryGetValue(id, out node);
            }

            public void Add(AStarNode node)
            {
                int id = m_Helper.GetUniqueId(node.Item);
                m_Nodes[id] = node;
            }
        }
    }
}
