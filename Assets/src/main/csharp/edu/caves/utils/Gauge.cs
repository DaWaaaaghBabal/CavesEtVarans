using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans 
{
    /**
     *  A Gauge is a floating value between a min and a max value. Incrementing
     *  or decrementing this value does not make it to go beyond its limits.
     */
    class Gauge 
    {
        private int min;
        private int max;
        private int value;

        public Gauge(int newMin, int newMax) 
        {
            min = newMin;
            max = newMax;
            value = min;
        }

        public int GetValue() 
        {
            return value;
        }

        internal bool CanBeIncreased(int qty)
        {
            return GetValue() + qty <= max; 
        }

        internal bool CanBeDecreased(int qty)
        {
            return GetValue() - qty >= min;
        }

        public void Increment(int value)
        {
            SetValue(GetValue() + value);
        }

        public void Decrement(int value)
        {
            SetValue(GetValue() - value);
        }

        public void SetValue(int newValue) 
        {
            if (newValue < min) 
            {
                value = min;
            } 
            else if (newValue > max) 
            {
                value = max;
            } 
            else 
            {
                value = newValue;
            }
        }

        public double getPercentage() 
        {
            return (value - min) / (max - min);
        }

        override public string ToString()
        {
            return value + "/" + max;
        }

    }
}


