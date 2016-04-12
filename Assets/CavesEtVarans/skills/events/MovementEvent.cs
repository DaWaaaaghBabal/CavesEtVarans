using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events {
	public class MovementEvent : GameEvent<MovementEvent> {
        public Character Source { private set; get; }
		public Tile StartTile { private set; get; }
		public Tile EndTile { private set; get; }
		public MovementEvent(Character source, Tile start, Tile end) {
			Source = source;
			StartTile = start;
			EndTile = end;
		}
	}
}