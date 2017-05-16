using CavesEtVarans.battlefield;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class EmptyTile : AbstractFilter {
        protected override bool FilterContext() {
            Tile t = ReadContext(ContextKeys.FILTER_TARGET) as Tile;
            return (t != null && t.IsFree);
        }
    }
}
