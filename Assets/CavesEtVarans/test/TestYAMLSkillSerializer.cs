using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CavesEtVarans.character.factions;
using CavesEtVarans.data;
using UnityEngine;
using YamlDotNet.Serialization;

namespace CavesEtVarans.test {
    public class TestYAMLSkillSerializer : MonoBehaviour {

        private StringBuilder SerializeObject(object obj) {
			var serializer = new Serializer(SerializationOptions.Roundtrip);
			var stringBuilder = new StringBuilder();
			var stringWriter = new StringWriter(stringBuilder);
			serializer.Serialize(stringWriter, obj);

			return stringBuilder;
		}

        void Start() {
            TestMissionDescriptor();
            YAMLDataManager mgr = new YAMLDataManager();
            MissionDescriptor descriptor = mgr.ParseTextResource<MissionDescriptor>("mission");
            Debug.Log(descriptor);
        }

        private void TestMissionDescriptor() {
            FactionDescriptor order = new FactionDescriptor() {
                Color="0000FF",
                Name="Ordre",
                FriendsAndFoes = new Dictionary<FactionDescriptor, FriendOrFoe>()
            };
            FactionDescriptor chaos = new FactionDescriptor() {
                Color="FF0000",
                Name="Chaos",
                FriendsAndFoes = new Dictionary<FactionDescriptor, FriendOrFoe>()
            };
            chaos.FriendsAndFoes.Add(order, FriendOrFoe.Foe);
            order.FriendsAndFoes.Add(chaos, FriendOrFoe.Foe);
            CharacterDescriptor paladin = new CharacterDescriptor() {
                Name = "Durandil",
                Race = "Elf",
                Class = "Paladin",
                Level = 1,
                Faction = order,
            };
            CharacterDescriptor berserker = new CharacterDescriptor() {
                Name = "Ra'argah",
                Race = "Orque",
                Class = "Berserker",
                Level = 1,
                Faction = chaos,
            };
            List<FactionDescriptor> factions = new List<FactionDescriptor>();
            factions.Add(order);
            factions.Add(chaos);
            List<CharacterDescriptor> characters = new List<CharacterDescriptor>();
            characters.Add(berserker);
            characters.Add(paladin);
            Print(new MissionDescriptor() {
                Factions = factions,
                Characters = characters
            });
        }

        private void Print(object obj) {
            StringBuilder builder = SerializeObject(obj);
            Debug.Log(builder.ToString());
        }
    }
}
