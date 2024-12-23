﻿
using System.Collections;
using System.Collections.Generic;

namespace Simon001.PathFinding
{
    public class AStarPath : IEnumerable<object>
    {
        private List<object> m_Items;

        public int Count => m_Items.Count;

        public object this[int index] => m_Items[index];

        internal AStarPath(AStarNode node)
        {
            m_Items = new List<object>();
            InnerRecursiveAdd(node);
        }

        public IEnumerator<object> GetEnumerator()
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
