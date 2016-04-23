using System.Collections.Generic;
using CavesEtVarans.character;

namespace CavesEtVarans.data {
	public class ClassManager : YAMLDataManager {
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
        public CharacterClass ClassByName(string className) {
            if (!classes.ContainsKey(className)) { 
                CharacterClass clazz = ParseTextResource<CharacterClass>(className);
                classes.Add(clazz.Name, clazz);
            }
			return classes[className];
		}
	}
}
