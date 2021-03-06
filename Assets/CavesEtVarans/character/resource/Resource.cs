﻿using CavesEtVarans.utils;
using System;

namespace CavesEtVarans.character.resource
{
	/**
     * A Ressource is something a character has in its intrinsic powers,
     * it judges of its abilities to pay to do an action.
     */
	public class Resource : Observable<Resource>
	{

		private Gauge gauge;
		public static readonly string AP = "AP";
		public static readonly string HP = "HP";

        public Resource (int min, int max)
		{
			gauge = new Gauge (min, max);
		}
		public int Value {
            get {
                return gauge.Value;
            }
            set {
                gauge.SetValue(value);
                Notify(this);
            }
        }

        public double Percentage {
            get {
                return gauge.Percentage;
            }
            private set { }
        }

        public bool CanBePaid (int qty) {
			return gauge.CanBeDecreased (qty);
		}

        public int Increment(int qty)
		{
			int newValue = gauge.Increase(qty);
			Notify(this);
			return newValue;
		}
	}
}

