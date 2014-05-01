using System;
using System.Collections.Generic;
using UnityEngine;
#if(!UNITY)
using System.IO;
using System.Reflection;
#endif
namespace CavesEtVarans
{
    // This class is usedd to read from properties files.
    // A properties file lists one property per line, as "key=value" pairs.
	public class Properties {
		private Dictionary<string, string> props;

        // Initialises the property object by reading a text file. 
        // The path given as a parameter is the path from the root of a /Resource folder.
        // The file extension mustn't be included.
		public Properties (string path) {
			props = new Dictionary<string, string> ();
#if (UNITY)
            string content = ReadFromUnity(path);
#else
            string content = ReadForTests(path);
#endif
			foreach (string line in content.Split('\n')) {
				if (!line.StartsWith ("#")) {
					string[] splitLine = line.Split ('=');
					props.Add (splitLine [0], splitLine [1]);
				}
			}
		}
#if (UNITY)

        private static string ReadFromUnity(string path) {
            TextAsset text = Resources.Load<TextAsset>(path);
            string content = text.text;
            Resources.UnloadAsset(text);
            return content;
        }
#else
        private string ReadForTests(string path) {
            string applicationPath = new System.Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            StreamReader file = new StreamReader(applicationPath + "/Assets/Resources/" + path + ".txt");
            string content = file.ReadToEnd();
            file.Close();
            return content;
        }
#endif
        // These methods return the value of the property referenced by the key, as a raw string or parsed to int or double.
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

