
namespace Simon001.PathFinding
{
    internal class AStarNode
    {
        private int m_HValue;
        private int m_GValue;
        private IAStarItem m_Item;

        public IAStarItem Item => m_Item;

        public AStarNode Parent { get; set; }

        public int GValue
        {
            get => m_GValue;
            set => m_GValue = value;
        }

        public int FValue => m_HValue + m_GValue;

        public int HValue => m_HValue;

        public AStarNode(IAStarItem item, int hValue)
        {
            m_Item = item;
            m_HValue = hValue;
            m_GValue = -1;
            Parent = null;
        }
    }
}
