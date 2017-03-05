namespace CavesEtVarans.skills.core {
    public enum SkillFlag {
        NoEvent, Reaction, SingleTarget,
        Offensive, Support, Defensive,
        Melee, Ranged
	}

    public enum TargetFlag {
        Ally, Friend, Foe, Neutral, Optional,
        IgnoreLoS
    }
}
