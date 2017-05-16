using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public enum StackingType {
		Independent, Replacement, WithDuration
	}

	public abstract class StackingStrategy {
		public virtual StackingType StackingType { get; private set; }

		public abstract void Stack(BuffInstance oldInstance, BuffInstance newInstance,
			Action<BuffInstance> AddBuffCallback, Action<BuffInstance> RemoveBuffCallback);

		public static StackingStrategy ProvideInstance(StackingType type) {
			switch (type) {
				case StackingType.Independent:
					return new Independent();
				case StackingType.Replacement:
					return new Replacement();
				case StackingType.WithDuration:
					return new StackWithDuration();
				default:
					return new Independent();
			}
		}


		private class Replacement : StackingStrategy {
			public override StackingType StackingType { get { return StackingType.Replacement; } }

			public override void Stack(BuffInstance oldInstance, BuffInstance newInstance,
				Action<BuffInstance> AddBuffCallback, Action<BuffInstance> RemoveBuffCallback) {

				RemoveBuffCallback(oldInstance);
				AddBuffCallback(newInstance);
			}
		}

		private class Independent : StackingStrategy {
			public override StackingType StackingType { get { return StackingType.Independent; } }

			public override void Stack(BuffInstance oldInstance, BuffInstance newInstance,
				Action<BuffInstance> AddBuffCallback, Action<BuffInstance> RemoveBuffCallback) {

				AddBuffCallback(newInstance);
			}
		}
		private class StackWithDuration : StackingStrategy {
			public override StackingType StackingType { get { return StackingType.WithDuration; } }

			public override void Stack(BuffInstance oldInstance, BuffInstance newInstance,
				Action<BuffInstance> AddBuffCallback, Action<BuffInstance> RemoveBuffCallback) {

				oldInstance.Stacks++;
				oldInstance.Duration = newInstance.Duration;
			}
		}
	}
}
