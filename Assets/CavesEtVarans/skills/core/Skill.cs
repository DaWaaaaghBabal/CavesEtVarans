using CavesEtVarans.character;
using CavesEtVarans.gui;
using CavesEtVarans.skills.events;
using CavesEtVarans.utils;
using System.Collections.Generic;
using System;
using CavesEtVarans.skills.effects;
using CavesEtVarans.battlefield;
using CavesEtVarans.skills.targets;

namespace CavesEtVarans.skills.core {
	/* About everything in the game, every action and every specificity of a character, is a Skill.
     * A skill is defined by a cost in various resources, targets, effects applied to these targets, and tests to check whether each target is affected.

     * The flow for using a skill is :
     * - activate the first target picker and wait for it to call back
     * - when the target picker calls back, activate the next one and wait
     * - when all target pickers have called back, pay the cost
     * - apply each test to a set of targets. For each target, the skill can be a miss, a hit, or a critical hit.
     * - apply each effect to a set of targets.
     * In fact, all targets and test results will be stored in the Context.
     */

	public class Skill : ContextDependent
	{


		public SkillAttributes Attributes { get; set; }
		public Cost Cost { set; get; }
		public List<TargetSelector> TargetPickers { set; get; }
		public List<IEffect> Effects { set; get; }
        public FlagsList<SkillFlag> Flags { set; get; }
		public Skill() {
			TargetPickers = new List<TargetSelector>();
			Effects = new List<IEffect>();
			Cost = new Cost();
			Attributes = new SkillAttributes();
            Flags = new FlagsList<SkillFlag>();
        }

		public void InitSkill (Context context)
		{
			targetPickerIndex = 0;
			NextTargetPicker (context);
		}

		int targetPickerIndex;
		public void NextTargetPicker (Context context)
		{
			if (targetPickerIndex < TargetPickers.Count) {
				targetPickerIndex++;
				TargetPickers [targetPickerIndex - 1].Activate (context);
			} else {
				UseSkill (context);
			}
		}
		private void ResetUI() {
            GUIEventHandler.Get().Reset();
		}

		/* Called when all targets have been picked and stored in the Context. Can 
		 * also be called by another object with a fully initialized Context to bypass target selection.
		 */
		public void UseSkill (Context context)
		{
			Character source = (Character)ReadContext(context, Context.SOURCE);
            if (source.CanPay(Cost)) { 
				PayCosts (context);
                if (!Flags[SkillFlag.NoEvent])
				    new SkillUseEvent(this, source).Trigger(context);
				ApplyEffects (context);
			} else {
				UnityEngine.Debug.Log("Cost couldn't be paid, skill use cancelled.");
			}
			ResetUI();
			ReactionCoordinator.Flush();
		}

		private void PayCosts (Context context)
		{
			((Character)ReadContext(context, Context.SOURCE)).Pay(Cost);
		}

		private void ApplyEffects (Context context)
		{
            foreach (IEffect effect in Effects) {
				effect.Apply (context);
			}
		}

        public void AddEffect(IEffect effect) {
			Effects.Add(effect);
		}

        public void AddTargetSelector(TargetSelector targetSelector)
        {
            TargetPickers.Add(targetSelector);
        }
    }
}
