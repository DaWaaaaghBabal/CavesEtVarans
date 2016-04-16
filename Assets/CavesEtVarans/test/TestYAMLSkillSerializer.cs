using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.effects;
using CavesEtVarans.skills.target;
using CavesEtVarans.skills.values;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using YamlDotNet.Serialization;

namespace CavesEtVarans.test {
	public class TestYAMLSkillSerializer : MonoBehaviour {
		private StringBuilder SerializeObject(System.Object obj) {
			var serializer = new Serializer(SerializationOptions.Roundtrip);
			var stringBuilder = new StringBuilder();
			var stringWriter = new StringWriter(stringBuilder);
			serializer.Serialize(stringWriter, obj);

			return stringBuilder;
		}

		void Start() {
			SerializeFullClass();
		}
		
		private void SerializeFullClass() {
			Skill basicAttack = SetupBasicAttack();
			Skill heal = SetupHeal();
			List<Skill> skills = new List<Skill>();
			skills.Add(basicAttack);
			skills.Add(heal);
			CharacterClass acolyt = new CharacterClass() {
				Name = "Acolyte",
				Health = 48,
				Defense = 8,
				Attack = 8,
				Damage = 8,
				Willpower = 4,
				Initiative = 8,
				Dodge = 8,
				Iterative = 20,
				Jump = 2,
				Energy = 8,
				EnergyName = "moral",
				SpecialName = "Enthousiasme",
				Special = 8,
				Skills = skills
			};
			Debug.Log(SerializeObject(acolyt));
		}
		private static Skill SetupHeal() {

			IValueCalculator healAmountCalculator = new StatReader() {
				Source = Context.SOURCE,
				Stat = Statistic.WILLPOWER
			};
			TargetPicker healTargetPicker = new TargetPicker() {
				TargetKey = "target1",
				SourceKey = Context.SOURCE,
				Range = 6,
				AoeRadius = 0,
				TargetNumber = 2,
				GroundTarget = false,
				PlayerChoice = PlayerChoiceType.PlayerChoice
			};
			SkillAttributes healAttributes = new SkillAttributes {
				Name = "Soin mineur",
				Icon = "heal",
				Description = "Soigne deux cibles à moins de 6 cases de [100% VOL].",
				Animation = "cast"
			};

			IEffect healEffect = new HealEffect() {
				Amount = healAmountCalculator,
				TargetKey = healTargetPicker.TargetKey
			};
			Skill heal = new Skill() {
				Attributes = healAttributes
			};
			heal.Cost.Add("AP", 25);
			heal.Effects.Add(healEffect);
			heal.TargetPickers.Add(healTargetPicker);
            heal.Flags[SkillFlag.OrientToTarget] = true;
            return heal;
		}

		private static Skill SetupBasicAttack() {
			IValueCalculator attackDamageCalculator = SetupDamageCalculator();
			TargetPicker attackTargetPicker = new TargetPicker() {
				TargetKey = "target1",
				SourceKey = Context.SOURCE,
				Range = 6,
				AoeRadius = 0,
				TargetNumber = 2,
				GroundTarget = false,
				PlayerChoice = PlayerChoiceType.PlayerChoice
			};
			SkillAttributes attackAttributes = new SkillAttributes {
				Name = "Attaque de base",
				Icon = "basicAttack",
				Description = "ATK contre DEF. Inflige [100% DMG] dégâts physiques à une cible au contact.",
				Animation = "attack"
			};
			IEffect damageEffect = new DamageEffect() {
				Amount = attackDamageCalculator,
				TargetKey = attackTargetPicker.TargetKey
			};
			Skill attack = new Skill() {
				Attributes = attackAttributes
			};
			attack.Cost.Add("AP", 25);
			attack.Effects.Add(damageEffect);
			attack.TargetPickers.Add(attackTargetPicker);
			return attack;
		}

		private static IValueCalculator SetupDamageCalculator() {
			return new ValueMultiplier() {
				Base = new StatReader() {
					Source = Context.SOURCE,
					Stat = Statistic.DAMAGE
				},
				Factor = new DamageFormula() {
					Attack = new StatReader() {
						Source = Context.SOURCE,
						Stat = Statistic.ATTACK
					},
					Defense = new StatReader() {
						Source = Context.CURRENT_TARGET,
						Stat = Statistic.DEFENSE
					}
				}
			};
		}
	}
}
