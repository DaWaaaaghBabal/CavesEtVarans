namespace CavesEtVarans.skills.core {
    public enum SkillFlag {
        OrientToTarget, NoEvent,
		Reaction, SingleTarget,
        Offensive, Support, Defensive, Melee
	}

    public enum TargetFlag {
        Ally, Friend, Foe, Neutral, Optional,
        IgnoreLoS
    }
}
