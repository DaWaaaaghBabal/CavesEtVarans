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
     * A skill is defined by a cost in various resources, targets, and effects applied to these targets.
     * The flow for using a skill is :
     * - activate the first target picker and wait for it to call back
     * - when the target picker calls back, activate the next one and wait
     * - when all target pickers have called back, pay the cost
     * - apply each effect to a set of targets.
     * All targets and test results will be stored in the Context.
     */

	public class Skill : ContextDependent
	{
		public SkillAttributes Attributes { get; set; }
		public Cost Cost { set; get; }
		public List<TargetSelector> TargetSelectors { set; get; }
		public List<IEffect> Effects { set; get; }
        public FlagsList<SkillFlag> Flags { set; get; }

		public Skill() {
			TargetSelectors = new List<TargetSelector>();
			Effects = new List<IEffect>();
			Cost = new Cost();
			Attributes = new SkillAttributes();
            Flags = new FlagsList<SkillFlag>();
        }

        /* Called from the interface, the most basic way to use a Skill : it goes through all the steps
         * of target selection.
         */
		public void InitSkill (Character source)
		{
            StartNewContext();
            SetContext(ContextKeys.SOURCE, source);
            SetContext(ContextKeys.SKILL, this);
			targetPickerIndex = 0;
			NextTargetPicker ();
		}
        /* Called when some data is determined before initiating the skill.
         */
        public void InitSkill (Dictionary<string, object> contextData) {
            StartNewContext();
            foreach (KeyValuePair<string, object> entry in contextData) {
                SetContext(entry.Key, entry.Value);
            }
            SetContext(ContextKeys.SKILL, this);
            targetPickerIndex = 0;
            NextTargetPicker();
        }

		/* Called when all targets have been picked and stored in the Context. Can 
		 * also be called by another object with a fully initialized Context to bypass target selection.
		 */
		public void UseSkill ()
		{
			Character source = (Character)ReadContext(ContextKeys.SOURCE);
            if (source.CanPay(Cost)) { 
				PayCosts ();
                if (!Flags[SkillFlag.NoEvent])
				    new SkillUseEvent(this, source).Trigger();
				ApplyEffects ();
			} else {
				UnityEngine.Debug.Log("Cost couldn't be paid, skill use cancelled.");
			}
			ResetUI();
            EndContext();
			ReactionCoordinator.Flush();
		}

        private int targetPickerIndex;
		public void NextTargetPicker ()
		{
			if (targetPickerIndex < TargetSelectors.Count) {
				targetPickerIndex++;
				TargetSelectors [targetPickerIndex - 1].Activate();
			} else {
				UseSkill ();
			}
		}

        public void AddEffect(IEffect effect) {
            Effects.Add(effect);
        }

        public void AddTargetSelector(TargetSelector targetSelector) {
            TargetSelectors.Add(targetSelector);
        }

		private void ResetUI() {
            GUIEventHandler.Get().Reset();
		}

		private void PayCosts ()
		{
			((Character)ReadContext(ContextKeys.SOURCE)).Pay(Cost);
		}

		private void ApplyEffects ()
		{
            foreach (IEffect effect in Effects) {
				effect.Apply();
			}
		}
    }
}
