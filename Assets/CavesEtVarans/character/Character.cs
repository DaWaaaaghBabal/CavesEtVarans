using CavesEtVarans.battlefield;
using CavesEtVarans.character.resource;
using CavesEtVarans.character.statistics;
using CavesEtVarans.gui;
using CavesEtVarans.skills.core;
using System;
using System.Collections.Generic;
using CavesEtVarans.skills.effects.buffs;
using CavesEtVarans.character.factions;
using CavesEtVarans.skills.events;
using CavesEtVarans.data;

namespace CavesEtVarans.character
{
    public class Character : ITargetable {

        public string Name { get; set; }
        public Faction Faction { get; set; }
        public int Level { get; set; }
        public String CharacterClass {
            get { return characterClass.Name; }
            set {
                InitClass(value);
            }
        }
		public FriendOrFoe FriendOrFoe(Character other) {
			return Faction.FriendOrFoe(other.Faction);
		}

        public void InitSkill(Skill skill) {
            skill.InitSkill(this);
        }

        public Tile Tile { get; set; }
        public Orientation Orientation { get; set; }
        public int Size { get; set; }

        private CharacterClass characterClass;
        private StatisticsManager statisticsManager;
        private ResourceManager resourceManager;
        private SkillManager skillManager;
        private BuffManager buffManager;
		private TriggerManager triggerManager;

		public Character() {
            statisticsManager = new StatisticsManager();
            resourceManager = new ResourceManager();
            skillManager = new SkillManager();
            buffManager = new BuffManager(this);
            resourceManager.Add(Resource.AP, new Resource(0, GetStatValue(Statistic.MAX_AP)));

			triggerManager = new TriggerManager(this);
			triggerManager.Register();
			skillManager.TriggerManager = triggerManager;
			skillManager.InitCommonSkills();
            Size = 3;
        }

        // Methods

		private void InitClass(string value) {
            characterClass = ClassManager.Instance.ClassByName(value);
            statisticsManager.InitClassStats(characterClass);
            skillManager.InitClassSkills(characterClass);

            int maxHealth = GetStatValue(Statistic.HEALTH);
            resourceManager.Add(Resource.HP, new Resource(0, maxHealth));
            resourceManager.Set(Resource.HP, maxHealth);
            foreach (KeyValuePair<string, int> KV in characterClass.HiddenResources) {
                resourceManager.Add(KV.Key, new Resource(0, KV.Value));
            }
        }

        public void Activate() {
            //@TODO do other stuff...
            new StartTurnEvent(this).Trigger();
			buffManager.HalfTick();
            MainGUI.ActivateCharacter(this);
        }

        public void EndTurn() {
            new EndTurnEvent(this).Trigger();
            buffManager.HalfTick();
            AP = AP / 2;
        }

        public void Pay(Cost cost) {
            cost.Pay(resourceManager);
        }

        public int TakeDamage(int amount) {
            int result = resourceManager.Increment(Resource.HP, - amount);
            if (resourceManager.GetAmount(Resource.HP) <= 0)
                Collapse();
            return result;
        }

        private void Collapse() {
            throw new NotImplementedException();
        }

        public void ApplyBuff(BuffInstance buff) {
            buffManager.ApplyBuff(buff);
        }

        public void RemoveBuff(BuffInstance buff) {
            buffManager.RemoveBuff(buff);
        }

        public int GetResourceAmount(String key) {
            return resourceManager.GetAmount(key);
        }

        public int GetStatValue(string key)
		{
			return statisticsManager.GetValue(key);
		}

		override public string ToString ()
		{
			return Name;
		}

		public int AP {
			get {
				return resourceManager.GetAmount(Resource.AP);
			}
			set {
				resourceManager.Set(Resource.AP, value);
			}
		}
		
		public bool CanPay(Cost cost) {
			return cost.CanBePaid(resourceManager);
		}

		public List<Skill> Skills {
			private set { }
			get { return skillManager.Skills; }
		}

		public void ApplyStatModifier(string key, IStatModifier modifier) {
			statisticsManager.AddModifier(key, modifier);
		}

		public void RemoveStatModifier(string key, IStatModifier modifier) {
			statisticsManager.RemoveModifier(key, modifier);
		}

        public int IncrementResource(string resourceKey, int amount) {
            return resourceManager.Increment(resourceKey, amount);
        }

        public Resource GetResource(string resourceKey) {
            return resourceManager.Get(resourceKey);
        }

        public EffectResult DispatchEffect(IEffect effect, int suffix) {
            return effect.Apply(this, suffix);
        }
        public void DispatchActivation(ITargetSelector selector, int suffix) {
            selector.Activate(Tile, Size, suffix);
        }
        public void DispatchTermination(ITargetSelector selector, int suffix) {
            selector.Terminate();
        }
    }

}


