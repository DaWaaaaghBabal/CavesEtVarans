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
using CavesEtVarans.skills.effects.buffs;
using CavesEtVarans.skills.triggers;
using CavesEtVarans.skills.events;
using CavesEtVarans.skills.triggers.filters;
using CavesEtVarans.character.factions;

namespace CavesEtVarans.test {
	public class TestYAMLSkillSerializer : MonoBehaviour {
		/*private StringBuilder SerializeObject(System.Object obj) {
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

			List<Skill> skills = new List<Skill>();
			skills.Add(SetupBasicAttack());
			skills.Add(SetupHeal());
			skills.Add(SetupBuff());
			List<TriggeredSkill> triggered = new List<TriggeredSkill>();
			triggered.Add(SetupTackle());
			CharacterClass acolyt = new CharacterClass() {
				Name = "Acolyte",
				Health = 48,
				Defense = 8,
				Attack = 8,
				Damage = 8,
				Willpower = 4,
				Initiative = 8,
				Stability = 8,
				Iterative = 20,
				Jump = 2,
				Energy = 8,
				EnergyName = "moral",
				SpecialName = "Enthousiasme",
				Special = 8,
				Skills = skills,
				TriggeredSkills = triggered,
			};
			Debug.Log(SerializeObject(acolyt));
		}

		private Skill SetupHeal() {

			IValueCalculator healAmountCalculator = new StatReader() {
				Source = Context.SOURCE,
				Stat = Statistic.WILLPOWER
			};
			TargetPicker healTargetPicker = new TargetPicker() {
				TargetKey = "target1",
				SourceKey = Context.SOURCE,
				Range = new FixedRange {Min = 0, Max = 5 },
				AoeRadius = new FixedRange (0, 1),
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

		private Skill SetupBasicAttack() {
			IValueCalculator attackDamageCalculator = SetupDamageCalculator();
			TargetPicker attackTargetPicker = new TargetPicker() {
				TargetKey = "target1",
				SourceKey = Context.SOURCE,
				Range = new FixedRange {Min = 0, Max = 5 },
				AoeRadius = new ZeroRange(),
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

		private Skill SetupBuff() {
			TargetPicker buffTargetPicker = new TargetPicker() {
				TargetKey = "target1",
				SourceKey = Context.SOURCE,
				Range = new FixedRange {Min = 0, Max = 5 },
				AoeRadius = new ZeroRange(),
				TargetNumber = 1,
				GroundTarget = false,
				PlayerChoice = PlayerChoiceType.PlayerChoice
			};
			SkillAttributes buffAttributes = new SkillAttributes {
				Name = "Buff de dégâts",
				Icon = "buff",
				Description = "Augmente les dégâts de la cible de 3.",
				Animation = "cast"
			};
			BuffEffect buffEffect = new StatIncrement() {
				StatKey = Statistic.DAMAGE,
				Value = new FixedValue() {
					Val = 3
				}
			};
			ApplyBuffEffect effect = new ApplyBuffEffect() {
				Duration = new Countdown() {
					Duration = 2
				},
				TargetKey = "target1"
			};
			effect.Effects.Add(buffEffect);
			effect.StackingType = StackingType.Independent;
			Skill buff = new Skill() {
				Attributes = buffAttributes
			};
			buff.Cost.Add("AP", 25);
			buff.Effects.Add(effect);
			buff.TargetPickers.Add(buffTargetPicker);
			return buff;
		}
		private TriggeredSkill SetupTackle() {
			TriggeredSkill triggeredSkill = new TriggeredSkill();

			IEffect damageEffect = new DamageEffect() {
				Amount = SetupDamageCalculator(),
				TargetKey = "target1"
			};
			TargetPicker targetPicker = new TargetPicker() {
				TargetKey = "target1",
				SourceKey = Context.TRIGGERING_CHARACTER,
				Range = new FixedRange {Min = 0, Max = 0 },
				AoeRadius = new ZeroRange(),
				TargetNumber = 1,
				GroundTarget = false,
				PlayerChoice = PlayerChoiceType.NoValidation
			};
			Skill tackle = new Skill() {
				Attributes = new SkillAttributes {
					Animation = "Attack",
				}
			};
			tackle.Effects.Add(damageEffect);
			tackle.TargetPickers.Add(targetPicker);
			triggeredSkill.TriggerType = TriggerType.Movement;
			triggeredSkill.Skill = tackle;
			triggeredSkill.TriggerFilters = new TriggerFiltersList();
			triggeredSkill.TriggerFilters.Add(new FriendOrFoeFilter() {
				Accepts = FriendOrFoe.Foe | FriendOrFoe.Neutral
			});
			return triggeredSkill;
		}
		private IValueCalculator SetupDamageCalculator() {
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
		}*/
	}
}
