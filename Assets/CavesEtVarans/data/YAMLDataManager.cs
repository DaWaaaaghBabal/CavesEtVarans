using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using YamlDotNet.Serialization;

namespace CavesEtVarans.data {
    public class YAMLDataManager {

        public T ParseTextResource<T>(string resourceName) {
            TextAsset textResource = Resources.Load<TextAsset>(resourceName);
            string yaml = textResource.text;
            //This is used to make the user-friendly YAML compliant with the deserializer. It requires adding a bunch of tokens to each class reference.
            yaml = Regex.Replace(yaml, "!(?<class>.*)\\r\\n", "!CavesEtVarans.${class},%20Assembly-CSharp,%20Version=0.0.0.0,%20Culture=neutral,%20PublicKeyToken=null\n");
            StringReader input = new StringReader(yaml);
            T t = new Deserializer().Deserialize<T>(input);
            return t;
        }
        
    }
}
