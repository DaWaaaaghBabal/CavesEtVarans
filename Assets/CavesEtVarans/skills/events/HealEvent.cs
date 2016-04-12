using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events
{
    public class HealEvent : GameEvent<HealEvent>
    {
        public int Amount;
        public Character Source;
        public Character Target;

        public HealEvent(Character source, Character target, int amount)
        {
            this.Source = source;
            this.Target = target;
            this.Amount = amount;
        }
    }
}