using System.Collections.Generic;

namespace CavesEtVarans.skills.core
{
    public class FlagsList<T> : List<T> {
        public bool this[T index]
        {
            get
            {
                return Contains(index);
            }
            set
            {
                if (value) Add(index);
                else Remove(index);
            }
        }
    }
}