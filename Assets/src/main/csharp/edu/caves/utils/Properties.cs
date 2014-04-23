using System;
using System.Collections.Generic;
using UnityEngine;
namespace CavesEtVarans
{
	public class Properties {
		private Dictionary<string, string> props;

		public Properties (string fileName) {
			props = new Dictionary<string, string> ();
			TextAsset text = Resources.Load<TextAsset> (fileName);
			string content = text.text;
			foreach (string line in content.Split('\n')) {
				if (!line.StartsWith ("#")) {
					string[] splitLine = line.Split ('=');
					props.Add (splitLine [0], splitLine [1]);
				}
			}
			Resources.UnloadAsset (text);
		}
		
		public int GetInt (string key) {
			return int.Parse (props [key]);
		}
		public string GetStr (string key) {
			return props [key];
		}
		public double getDbl (string key) {
			return double.Parse (props [key]);
		}
	}
}

