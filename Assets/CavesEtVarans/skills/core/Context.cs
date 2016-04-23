using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using System;
using System.Collections.Generic;

namespace CavesEtVarans.skills.core
{
	
	public class Context
    {
		public static readonly string SOURCE = "source";
        public static readonly string TARGETS = "targets/";
        public static readonly string CURRENT_TARGET = "currentTarget";
        public static readonly string MOVEMENT_TARGET = "mvtTarget";
		public static readonly string BUFF_SOURCE = "buffSource";
		public static readonly string BUFF_TARGET = "buffTarget";
		public static readonly string SKILL = "skill";
		public static readonly string TILE = "tile";
        public static readonly string DAMAGE_TYPE = "damageType";
		public static readonly string TRIGGERING_SKILL = "triggeringSkill";
		public static readonly string TRIGGERING_TARGETS = "triggeringTarget";
		public static readonly string TRIGGERING_CHARACTER = "triggeringCharacter";
        public static readonly string FILTER_TARGET = "filterTarget";
		public static readonly string START_TILE = "startTile";
		public static readonly string END_TILE = "endTile";

		private Dictionary<string, Object> data;
		public static readonly Context Empty = new Context();

        private Context ()
		{
			data = new Dictionary<string, object> ();
		}

		/// <summary>
		/// Returns an iterable of all data stored with keys that start with the argument key.
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public IEnumerable<object> AllKeys(string start) {
			HashSet<object> result = new HashSet<object>();
			foreach (string key in data.Keys) {
				if (key.StartsWith(start)) {
					result.Add(data[key]);
				}
			}
			return result;
		}

		public static Context Init (Skill skill, Character source)
		{
			Context context = new Context ();
			context.Set (SOURCE, source);
			context.Set (SKILL, skill);
			return context;
		}

		public void Set (string key, Object value)
		{
			data.Remove(key);
			data.Add(key, value);
		}
		// Retrieves whatever is stored in the Context under a given key. Returns null if there is nothing.
		private Object Get (string key)
		{
			return data.ContainsKey(key) ? data [key] : null;
		}

		// Retrieves whatever is stored in the Context under a given key. Gives priority to the context-dependent object's private context.
		public Object Get (string key, Context privateContext) {
			Object result = privateContext.Get(key);
			return result != null ? result : Get(key);
        }
		
		public void CopyInto(Context target) {
			foreach (KeyValuePair<string, object> kv in data) {
				target.Set(kv.Key, kv.Value);
			}
		}

		public Context Duplicate() {
			Context copy = new Context();
			CopyInto(copy);
			return copy;
		}
		
		public static Context ProvideTurnOrderContext() {
            return new Context();
        }
        public static Context ProvidePrivateContext(ContextDependent contextDependent) {
            return new Context();
        }
        public static Context ProvideMovementContext(Character character, Tile targetTile) {
            Context context = new Context();
            // The character is both the source, as he is using the movement action, 
            // and the target, as he is the one that is moved.
            context.Set(SOURCE, character);
            TargetGroup targets = new TargetGroup(character);
            context.Set(TARGETS + MOVEMENT_TARGET, targets);

            targets = new TargetGroup(targetTile);
            context.Set(TARGETS + TILE, targets);
            return context;
        }
        public static Context ProvideDisplayContext() {
            return Init(null, null);
        }
    }
}
