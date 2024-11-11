
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
            AStarNode startNode = new AStarNode(startItem, m_Helper.GetHValue(startItem, endItem));
            startNode.OriginGValue = 1;
            startNode.GValue = 1;
            AStarNode endNode = null;
            m_OpenList.Add(startNode);
            int count = 10000;
            while (!m_OpenList.Empty && count-- > 0)
            {
                AStarNode itemNode = m_OpenList.RemoveMinimum();

                if (itemNode.Item == endItem)
                {
                    endNode = itemNode;
                    break;
                }

                Logger($"itemNode ({m_OpenList.Count}) {itemNode.Item} {itemNode.OriginGValue} {itemNode.FValue} {count} ");

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
                        childNode = new AStarNode(child, hValue);
                        m_OpenList.Add(childNode);
                    }

                    if (childNode != null)
                    {
                        int gValue = m_Helper.GetGValue(itemNode.Item, childNode.Item);
                        if(gValue < childNode.OriginGValue)
                        {
                            childNode.OriginGValue = gValue;
                        }
                        
                        gValue += itemNode.GValue;
                        if (gValue < childNode.GValue)
                        {
                            childNode.Parent = itemNode;
                            childNode.GValue = gValue;


                            Logger($"eee {childNode.Item} {childNode.OriginGValue} {childNode.HValue} {childNode.FValue} ");

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
