using System;
using System.Collections.Generic;

namespace CavesEtVarans.character.resource
{
	/*
     * The role of the ResourceManager is to track the behavior of all 
     * resource components of a character, and to inform any observer of
     * any change.
     * The filling of resources from a character is done when the 
     * character's data is parsed
     */
	public class ResourceManager
	{
		protected Dictionary<string, Resource> resources;
		public void Add (string key, Resource res)
		{
			resources.Add (key, res);
		}
		public ResourceManager() {
				resources = new Dictionary<string, Resource>();
		}

		public int GetAmount (string key)
		{
			if (resources.ContainsKey (key)) {
				return resources [key].GetValue();
			} else {
				throw new Exception("The wanted ressource (" + key + ") does not exist.");
			}
		}
		public void Set (string key, int newValue) {
			if (resources.ContainsKey(key)) {
				resources[key].SetValue(newValue);
			} else {
				throw new Exception("The wanted ressource (" + key + ") does not exist.");
			}
		}

		public void Increment(string key, int amount) {
			if (resources.ContainsKey(key)) {
				resources[key].Increment(amount);
			} else {
				throw new Exception("The wanted ressource (" + key + ") does not exist.");
			}
		}

		public Boolean CanBePaid (String key, int amount)
		{
			return (resources[key].CanBePaid(amount));
		}

	}

}
