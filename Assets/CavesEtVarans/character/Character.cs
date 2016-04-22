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

namespace CavesEtVarans.character
{
    public class Character : ITargetable {

        public string Name { get; set; }
        public Faction Faction { get; set; }
        public int Level { get; set; }
        public String CharacterClass {
            get { return clazz.Name; }
            set {
                InitClass(value);
            }
        }
		public FriendOrFoe FriendOrFoe(Character other) {
			return Faction.FriendOrFoe(other.Faction);
		}

		public Tile Tile { get; set; }
        public Orientation Orientation { get; set; }
        public int Size { get; set; }

        private CharacterClass clazz;
        private StatisticsManager statisticsManager;
        private ResourceManager resourceManager;
        private SkillManager skillManager;
        private BuffManager buffManager;
		private TriggerManager triggerManager;

		public Character() {
            Context context = Context.Init(null, this);
            statisticsManager = new StatisticsManager();
            resourceManager = new ResourceManager();
            skillManager = new SkillManager();
            buffManager = new BuffManager(this);
            resourceManager.Add(Resource.AP, new Resource(0, GetStatValue(Statistic.MAX_AP, context)));

			triggerManager = new TriggerManager(this);
			triggerManager.Register();
			skillManager.TriggerManager = triggerManager;
			skillManager.InitCommonSkills();
            Size = 3;
        }

        // Methods

		private void InitClass(string value) {
            clazz = ClassManager.Instance.ClassByName(value);
            statisticsManager.InitClassStats(clazz);
            skillManager.InitClassSkills(clazz);
            int maxHealth = GetStatValue(Statistic.HEALTH, Context.Init(null, this));
            resourceManager.Add(Resource.HP, new Resource(0, maxHealth));
            resourceManager.Set(Resource.HP, maxHealth);
        }

        public void Activate() {
            //@TODO do other stuff...
            MainGUI.ActivateCharacter(this);
			buffManager.Tick();
            new StartTurnEvent(this).Trigger(Context.Empty);
        }

        public void EndTurn() {
            AP = AP / 2;
        }

        public void Pay(Cost cost) {
            cost.Pay(resourceManager);
        }

        public void TakeDamage(int amount) {
            resourceManager.Increment(Resource.HP, - amount);
        }

        public void ApplyBuff(BuffInstance buff, Context context) {
            buffManager.ApplyBuff(buff, context);
        }

        public void RemoveBuff(BuffInstance buff) {
            buffManager.RemoveBuff(buff);
        }

        public int GetResourceAmount(String key) {
            return resourceManager.GetAmount(key);
        }

        public int GetStatValue(string key, Context context)
		{
			return statisticsManager.GetValue(key, context);
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

        public void IncrementResource(string resourceKey, int amount) {
            resourceManager.Increment(resourceKey, amount);
        }

	}

}


