using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.skills.events {

	/**
	 * Reaction skills are resolved after all effects of the triggering skill. 
	 * Reactions to these reactions are resolved after all reactions to a skill are resolved,
	 * in the order in which they have been triggered.
	 */
	public class ReactionCoordinator {

		private class Entry {
			public Skill Skill { set; get; }
			public Context Context { set; get; }
			public Entry(Skill skill, Context context) {
				this.Skill = skill;
				this.Context = context;
			}
		}

		private Queue<Entry> currentQueue;
		private Queue<Entry> nextQueue;

		private ReactionCoordinator() {
			currentQueue = new Queue<Entry>();
			nextQueue = new Queue<Entry>();
		}

		private void FileEntry(Entry entry) {
			nextQueue.Enqueue(entry);
		}

		private void FlushQueue() {
			if (currentQueue.Count == 0) {
				currentQueue = nextQueue;
				nextQueue = new Queue<Entry>();
			}
			if (currentQueue.Count != 0) {
				Entry firstEntry = currentQueue.Dequeue();
				Skill skill = firstEntry.Skill;
				Context context = firstEntry.Context;
				skill.UseSkill(context);
			}
		}

		private static ReactionCoordinator instance = new ReactionCoordinator();

		/** Informs the reaction coordinator that all effects of a skill have been applied, 
		 * i.e, all reaction skills have been triggered and filed. The reaction coordinator will then
		 * decide what to do next to apply the next skill.
		 */
		public static void Flush() {
			instance.FlushQueue();
		}

		/** Informs the reaction coordinator that an event triggered a reaction skill
		 * and it must be resolved. The coordinator will activate the skill
		 * when the time comes.
		 */
		public static void FileReaction(Skill reactionSkill, Context reactionContext) {
			instance.FileEntry (new Entry(reactionSkill, reactionContext));
		}
	}
}
