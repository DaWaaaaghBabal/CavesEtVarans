namespace CavesEtVarans.battlefield {
	public class Orientation {
        private Orientation left;


        public Orientation Left {
            get {
                return left;
            }
            set {
                left = value;
                value.Right = this;
            }
        }
		public Orientation Right { get; private set; }
        public int LeftDistance (Orientation target) {
            return target == this ? 0 : 1 + Left.LeftDistance(target);
        }
		
    }
}