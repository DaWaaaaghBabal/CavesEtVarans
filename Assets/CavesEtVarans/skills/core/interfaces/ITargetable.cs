using CavesEtVarans.battlefield;

namespace CavesEtVarans.skills.core {
	public interface ITargetable {
        int Size { get; set; }

        /// <summary>
        /// Used in cases when the target position on the game grid
        /// is useful, like orienting source towards target
        /// </summary>
        Tile Tile { set; get; }
        /// <summary>
        /// Applies an effect on this Targetable. Dispatches based on the exact nature of the Targetable :
        /// Tile and Character apply it on themselves, TargetGroup on each TargetGroup it contains.
        /// </summary>
        /// <param name="effect">The effect to apply.</param>
        /// <param name="suffix">This suffix will be appended to the end of the targeting key to associate multiple targets
        /// in the case of effects that use several targets.</param>
        EffectResult DispatchEffect(IEffect effect, int suffix);
        /// <summary>
        /// Activates a TargetSelector centered on this Targetable. Dispatches based on the exact nature of the Targetable :
        /// Tile and Character activate the selector once, TargetGroup activates it once centered on each element it contains
        /// (following the normal activation - user response - termination process).
        /// </summary>
        /// <param name="selector">The selector to activate.</param>
        /// <param name="suffix">This suffix will be appended to the end of the targeting key to associate multiple targets
        /// in the case of effects that use several targets.</param>
        void DispatchActivation(ITargetSelector selector, int suffix);
        /// <summary>
        /// Finalizes target selection for a TargetSelector. Dispatches based on the exact nature of the Targetable :
        /// Tile and Character just end selection, TargetGroup activates the Selector centered on the next element it contains.
        /// </summary>
        /// <param name="selector">The selector to activate.</param>
        /// <param name="suffix">This suffix will be appended to the end of the targeting key to associate multiple targets
        /// in the case of effects that use several targets.</param>
        void DispatchTermination(ITargetSelector selector, int suffix);
	}
}