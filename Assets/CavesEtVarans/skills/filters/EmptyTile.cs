using CavesEtVarans.battlefield;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class EmptyTile : AbstractFilter {
        protected override bool FilterContext(Context c) {
            Tile t = ReadContext(c, Context.FILTER_TARGET) as Tile;
            return (t != null && t.IsFree);
        }
    }
}
