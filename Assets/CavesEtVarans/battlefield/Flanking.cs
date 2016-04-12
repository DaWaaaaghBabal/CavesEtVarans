using System;

namespace CavesEtVarans.battlefield {
	[Flags]
	public enum Flanking {
		Front = 0x0,
		FrontFlank = 0x1,
		Flank = 0x2,
		BackFlank = 0x4,
		Back = 0x8
	}
}