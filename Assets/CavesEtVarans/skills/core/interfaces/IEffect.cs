using CavesEtVarans.battlefield;
using CavesEtVarans.character;

namespace CavesEtVarans.skills.core {
	public interface IEffect
    {
        void Apply();
        /// <summary>
        /// Applies this effect on a character.
        /// </summary>
        /// <param name="character">The target.</param>
        /// <param name="suffix">Used to synchronize several targets, for example in the case of a movement effect (target character + target tile).
        /// Any secondary target must use its target key + suffix.</param>
        /// <returns></returns>
        EffectResult Apply(Character character, int suffix);
        /// <summary>
        /// Applies this effect on a tile.
        /// </summary>
        /// <param name="character">The target.</param>
        /// <param name="suffix">Used to synchronize several targets, for example in the case of a movement effect (target character + target tile).
        /// Any secondary target must use its target key + suffix.</param>
        /// <returns></returns>
        EffectResult Apply(Tile tile, int suffix);
    }
}
