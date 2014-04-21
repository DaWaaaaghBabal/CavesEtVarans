package combat;

import java.util.HashSet;
import java.util.Set;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.skills.Skill;
import com.dwb.caves.control.skills.SkillContext;
import com.dwb.caves.control.skills.SkillTargetSelector;

public class RepeatedCombatTest_Selector extends SkillTargetSelector {

	public GameCharacter target;
	
	public RepeatedCombatTest_Selector(Skill skill) {
		super(skill);
	}

	public void pickTargets(SkillContext context) {
		Set<GameCharacter> targets = new HashSet<GameCharacter>();
		targets.add(target);
		context.put("targets", targets);
		skill.resolve(context);
	}

}
