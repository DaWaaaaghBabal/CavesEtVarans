using CavesEtVarans.exceptions;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using YamlDotNet.Serialization;

namespace CavesEtVarans.character {
	public class ClassManager {
		private static ClassManager instance;
		public static ClassManager Instance {
			private set { }
			get {
				if (instance == null)
					instance = new ClassManager();
				return instance;
			}
		}
		private Dictionary<string, CharacterClass> classes;

		private ClassManager() {
			classes = new Dictionary<string, CharacterClass>();
		}

		public void ParseTextResource(string resourceName) {
			TextAsset textResource = Resources.Load<TextAsset>(resourceName);
			string yaml = textResource.text;
			//This is used to make the user-friendly YAML compliant with the deserializer. It requires adding a bunch of tokens to each class reference.
			yaml = Regex.Replace(yaml, "!(?<class>.*)\\r\\n", "!CavesEtVarans.${class},%20Assembly-CSharp,%20Version=0.0.0.0,%20Culture=neutral,%20PublicKeyToken=null\n");
			StringReader input = new StringReader(yaml);
			ClassList list = new Deserializer().Deserialize<ClassList>(input);
			foreach(CharacterClass clazz in list.Classes) {
				classes.Add(clazz.Name, clazz);
			}
		}

		public CharacterClass ClassByName(string className) {
			if (!classes.ContainsKey(className))
				throw new CharacterClassException("No character class found with this name.");
			return classes[className];
		}
	}
}
