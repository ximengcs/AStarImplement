
using System.Collections;
using System.Collections.Generic;

namespace Simon001.PathFinding
{
    public class AStarPath : IEnumerable<IAStarItem>
    {
        private List<IAStarItem> m_Items;

        internal AStarPath(AStarNode node)
        {
            m_Items = new List<IAStarItem>();
            InnerRecursiveAdd(node);
        }

        public IEnumerator<IAStarItem> GetEnumerator()
        {
            return m_Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Items.GetEnumerator();
        }

        private void InnerRecursiveAdd(AStarNode node)
        {
            if (node == null) return;
            m_Items.Add(node.Item);
            InnerRecursiveAdd(node.Parent);
        }
    }
}
