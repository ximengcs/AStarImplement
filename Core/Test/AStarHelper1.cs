#if TEST_IMPLEMENT
namespace Simon001.PathFinding.Test
{
    public class AStarHelper1 : IAStarHelper
    {
        public int GetGValue(IAStarItem from, IAStarItem to)
        {
            MapDataItem fromItem = from as MapDataItem;
            MapDataItem toItem = to as MapDataItem;
            Vector2Int pos = toItem.Pos - fromItem.Pos;
            if (Mathf.Abs(pos.x) > 0 && Mathf.Abs(pos.y) > 0)
                return 14;
            else
                return 10;
        }

        public int GetHValue(IAStarItem from, IAStarItem to)
        {
            MapDataItem fromItem = from as MapDataItem;
            MapDataItem toItem = from as MapDataItem;
            Vector2Int pos = toItem.Pos - fromItem.Pos;
            return Mathf.Abs(pos.x) + Mathf.Abs(pos.y);
        }

        public void GetItemRound(IAStarItem item, List<IAStarItem> result)
        {
            MapDataItem mapItem = item as MapDataItem;
            if (mapItem != null)
            {
                Vector2Int pos = mapItem.Pos;

                int x = pos.x;
                int y = pos.y;

                MapDataItem t = GameModule.MapEditor.GetItem(x, y + 1);
                MapDataItem b = GameModule.MapEditor.GetItem(x, y - 1);
                MapDataItem l = GameModule.MapEditor.GetItem(x - 1, y);
                MapDataItem r = GameModule.MapEditor.GetItem(x + 1, y);

                MapDataItem lt = GameModule.MapEditor.GetItem(x - 1, y + 1);
                MapDataItem rt = GameModule.MapEditor.GetItem(x + 1, y + 1);

                MapDataItem lb = GameModule.MapEditor.GetItem(x - 1, y - 1);
                MapDataItem rb = GameModule.MapEditor.GetItem(x + 1, y - 1);

                if (t != null && t.Meta.CanPass) result.Add(t);
                if (b != null && b.Meta.CanPass) result.Add(b);
                if (l != null && l.Meta.CanPass) result.Add(l);
                if (r != null && r.Meta.CanPass) result.Add(r);

                if (lt != null && lt.Meta.CanPass && l.Meta.CanPass && t.Meta.CanPass) result.Add(lt);
                if (rt != null && rt.Meta.CanPass && r.Meta.CanPass && t.Meta.CanPass) result.Add(rt);
                if (lb != null && lb.Meta.CanPass && l.Meta.CanPass && b.Meta.CanPass) result.Add(lb);
                if (rb != null && rb.Meta.CanPass && r.Meta.CanPass && b.Meta.CanPass) result.Add(rb);
            }
        }

        public int GetUniqueId(IAStarItem item)
        {
            MapDataItem mapItem = item as MapDataItem;
            return mapItem.Pos.x | (mapItem.Pos.y << 16);
        }
    }
}
#endif