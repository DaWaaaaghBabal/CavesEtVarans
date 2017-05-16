using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.skills.events {

	/**
	 * Reaction skills are resolved after all effects of the triggering skill. 
	 * Reactions to these reactions are resolved after all reactions to a skill are resolved,
	 * in the order in which they have been triggered.
	 */
	public class ReactionCoordinator {

		private static ReactionCoordinator instance = new ReactionCoordinator();
		private Queue<KeyValuePair<Skill, Dictionary<string, object>>> currentQueue;
		private Queue<KeyValuePair<Skill, Dictionary<string, object>>> nextQueue;

		private ReactionCoordinator() {
			currentQueue = new Queue<KeyValuePair<Skill, Dictionary<string, object>>>();
			nextQueue = new Queue<KeyValuePair<Skill, Dictionary<string, object>>>();
		}

		/** Informs the reaction coordinator that an event triggered a reaction skill
		 * and it must be resolved. The coordinator will activate the skill
		 * when the time comes.
		 */
		public static void FileReaction(Skill skill, Dictionary<string, object> reactionData) {
			instance.nextQueue.Enqueue(new KeyValuePair<Skill, Dictionary<string, object>>(skill, reactionData));
		}

		private void FlushQueue() {
			if (currentQueue.Count == 0) {
				currentQueue = nextQueue;
				nextQueue = new Queue<KeyValuePair<Skill, Dictionary<string, object>>>();
			}
			if (currentQueue.Count != 0) {
                KeyValuePair<Skill, Dictionary<string, object>> entry = currentQueue.Dequeue();
                entry.Key.InitSkill(entry.Value);
			}
		}

		/** Informs the reaction coordinator that all effects of a skill have been applied, 
		 * i.e, all reaction skills have been triggered and filed. The reaction coordinator will then
		 * decide what to do next to apply the next skill.
		 */
		public static void Flush() {
			instance.FlushQueue();
		}

	}
}
