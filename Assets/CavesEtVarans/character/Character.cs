using CavesEtVarans.battlefield;
using CavesEtVarans.character.resource;
using CavesEtVarans.character.statistics;
using CavesEtVarans.gui;
using CavesEtVarans.skills.core;
using System;
using System.Collections.Generic;
using CavesEtVarans.skills.effects.buffs;

namespace CavesEtVarans.character
{
	public class Character : ITargetable
	{

		public string Name { get; set; }
		public int Level { get; set; }
		public String CharacterClass {
			get { return clazz.Name; }
			set {
				clazz = ClassManager.Instance.ClassByName(value);
				skillManager.InitClassSkills(clazz);
			}
		}
		public Tile Tile { get; set; }
		public Orientation Orientation { get; set; }

		private CharacterClass clazz;
		private StatisticsManager statisticsManager;
		private ResourceManager resourceManager;
		private SkillManager skillManager;
		private BuffManager buffManager;

		public Character() {
			Context context = Context.Init(null, this);
			statisticsManager = new StatisticsManager();
			resourceManager = new ResourceManager();
			skillManager = new SkillManager();
			buffManager = new BuffManager(this);
			resourceManager.Add(Resource.AP, new Resource(0, GetStatValue(Statistic.MAX_AP, context)));

			int maxHealth = GetStatValue(Statistic.HEALTH, context);
			resourceManager.Add(Resource.HP, new Resource(0, maxHealth));
			resourceManager.Set(Resource.HP, maxHealth);

			skillManager.InitCommonSkills();
		}
		
		// Methods
		public void Activate()
		{
			//@TODO do other stuff...
			MainGUI.ActivateCharacter (this);
		}

		public void EndTurn()
		{
			AP = AP / 2;
		}
		
		public void Pay (Cost cost)
		{
			cost.Pay(resourceManager);
		}
		
		public void TakeDamage(int amount) {
			//@TODO event
			resourceManager.Decrement(Resource.HP, amount);
		}

		public void ApplyBuff(BuffInstance buff, Context context) { 
			//@TODO event
			buffManager.ApplyBuff(buff, context);
		}

		public void RemoveBuff(BuffInstance buff) {
			//@TODO event
			buffManager.RemoveBuff(buff);
		}

		public int GetResourceAmount(String key)
		{
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
		public void IncrementAP(int amount) {
			resourceManager.Increment(Resource.AP, amount);
		}
		
		public bool CanPay(Cost cost) {
			return cost.CanBePaid(resourceManager);
		}

		public List<Skill> Skills {
			private set { }
			get { return skillManager.Skills; }
		}
	}

}


