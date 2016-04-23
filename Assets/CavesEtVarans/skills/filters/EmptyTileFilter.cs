using CavesEtVarans.battlefield;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class EmptyTileFilter : AbstractFilter {
        public override bool Filter(Context c) {
            Tile t = ReadContext(c, Context.FILTER_TARGET) as Tile;
            return (t != null && t.IsFree);
        }
    }
}
