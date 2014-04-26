using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans
{
    /**
     * A Ressource is something a character has in its intrinsic powers,
     * it judges of its abilities to pay to do an action.
     */
    class Resource : Observable<Resource>
    {
        private Gauge gauge;

        public Resource(int min, int max)
        {
            gauge = new Gauge(min, max);
        }

        public int GetValue() 
        {
            return gauge.GetValue();
        }

        internal void SetValue(int newValue)
        {
            gauge.SetValue(newValue);
            Notify(this);
        }

        public Boolean CanBePaid(int qty)
        {
            return gauge.CanBeDecreased(qty);
        }

        public void Pay(int qty) 
        {
            if(CanBePaid(qty))
            {
                gauge.Decrement(qty);
            }
            else
            {
                // should never happen
                throw new Exception("This cost (" + qty + ") couldn't be paid : " + gauge.GetValue() + " in stock.");
            }
            Notify(this);
        }

        public void Give(int qty)
        {   
            gauge.Increment(qty);
            Notify(this);
        }

    }

}

