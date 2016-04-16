using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using System;
using System.Collections.Generic;

namespace CavesEtVarans.skills.core
{
	
	public class Context
    {
        public static readonly string CURRENT_TARGET = "currentTarget";
        public static readonly string MOVEMENT_TARGET = "mvtTarget";
        public static readonly string TARGETS = "target";
		public static readonly string SOURCE = "source";
		public static readonly string BUFF_TARGET = "buffTarget";
		public static readonly string SKILL = "skill";
		public static readonly string TILE = "tile";
        public static readonly string DAMAGE_TYPE = "damageType";

		private Dictionary<string, Object> data;

		private Context ()
		{
			data = new Dictionary<string, object> ();
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
