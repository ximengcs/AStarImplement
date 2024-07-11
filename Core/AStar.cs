
namespace Simon001.PathFinding
{
    public partial class AStar
    {
        private IAStarItem m_StartItem;
        private IAStarHelper m_Helper;
        private NodeCollection m_OpenList;
        private NodeCollection m_CloseList;
        private List<IAStarItem> m_Cache;

        public AStar(IAStarHelper helper, IAStarItem startItem)
        {
            m_Helper = helper;
            m_StartItem = startItem;
            m_OpenList = new NodeCollection(m_Helper);
            m_CloseList = new NodeCollection(m_Helper);
            m_Cache = new List<IAStarItem>();
        }

        public AStarPath Execute(IAStarItem endItem)
        {
            AStarNode endNode = null;
            bool finish = false;
            m_OpenList.Add(new AStarNode(m_StartItem, m_Helper.GetHValue(m_StartItem, endItem)));
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
                        childNode = new AStarNode(child, m_Helper.GetHValue(child, endItem));
                        m_OpenList.Add(childNode);
                    }

                    int gValue = m_Helper.GetGValue(itemNode.Item, childNode.Item);
                    if (gValue != -1)
                    {
                        int newFValue = gValue + itemNode.GValue + childNode.HValue;
                        if (childNode.GValue == -1 || childNode.FValue > newFValue)
                        {
                            childNode.Parent = itemNode;
                            childNode.GValue = newFValue;
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

            AStarPath path = null;
            if (endNode != null)
            {
                path = new AStarPath(endNode);
            }
            return path;
        }
    }
}
