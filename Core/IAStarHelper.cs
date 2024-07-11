
using System.Collections.Generic;

namespace Simon001.PathFinding
{
    public interface IAStarHelper
    {
        void GetItemRound(IAStarItem item, List<IAStarItem> result);

        int GetUniqueId(IAStarItem item);

        int GetHValue(IAStarItem from, IAStarItem to);

        int GetGValue(IAStarItem from, IAStarItem to);
    }
}
