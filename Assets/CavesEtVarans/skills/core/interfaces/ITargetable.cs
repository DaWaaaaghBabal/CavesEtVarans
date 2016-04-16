using CavesEtVarans.battlefield;

namespace CavesEtVarans.skills.core {
	public interface ITargetable {
        int Size { get; set; }

        /// <summary>
        /// Used in cases when the target position on the game grid
        /// is useful, like orienting source towards target
        /// </summary>
        Tile Tile { set; get; }
	}
}