
using System;
using System.Collections.Generic;

namespace Simon001.PathFinding
{
    public partial class AStar
    {
        public const int INVALID = -1;
        public const int MAX_VALUE = int.MaxValue;

        private IAStarHelper m_Helper;
        private NodeCollection m_OpenList;
        private NodeCollection m_CloseList;
        private HashSet<IAStarItem> m_Cache;

        private Action<string> Logger;

        public AStar(IAStarHelper helper, Action<string> logger)
        {
            Logger = logger;
            m_Helper = helper;
            m_OpenList = new NodeCollection(m_Helper);
            m_CloseList = new NodeCollection(m_Helper);
            m_Cache = new HashSet<IAStarItem>();
        }

        public AStarPath Execute(IAStarItem startItem, IAStarItem endItem)
        {
            AStarNode endNode = null;
            bool finish = false;
            m_OpenList.Add(new AStarNode(startItem, m_Helper.GetHValue(startItem, endItem)));
            int count = 10000;
            while (!m_OpenList.Empty && !finish && count-- > 0)
            {
                AStarNode itemNode = m_OpenList.RemoveMinimum();
                m_CloseList.Add(itemNode);

                m_Cache.Clear();
                m_Helper.GetItemRound(itemNode.Item, m_Cache);

                foreach (IAStarItem child in m_Cache)
                {
                    if (m_CloseList.Contains(child))
                        continue;
                    if (!m_OpenList.TryGet(child, out AStarNode childNode))
                    {
                        int hValue = m_Helper.GetHValue(child, endItem);
                        if (hValue != INVALID)
                        {
                            childNode = new AStarNode(child, hValue);
                            m_OpenList.Add(childNode);
                        }
                    }

                    if (childNode != null)
                    {
                        int gValue = m_Helper.GetGValue(itemNode.Item, childNode.Item);
                        if (gValue != INVALID)
                        {
                            int newFValue = gValue + childNode.HValue;
                            if (childNode.GValue == INVALID || childNode.FValue > newFValue)
                            {
                                Logger($"compare {childNode.GetHashCode()}  {childNode.FValue} {childNode.GValue}  -->  g {gValue} h {childNode.HValue} {newFValue} ");
                                childNode.Parent = itemNode;
                                childNode.GValue = gValue;
                            }
                        }

                        if (child == endItem)
                        {
                            finish = true;
                            endNode = childNode;
                            break;
                        }
                    }
                }
            }

            AStarPath path = null;
            if (endNode != null)
            {
                path = new AStarPath(endNode);
            }
            return path;
        }
    }
}
