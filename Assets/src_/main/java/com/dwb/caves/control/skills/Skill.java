package com.dwb.caves.control.skills;

import java.util.List;
import java.util.Observable;
import java.util.Set;

import com.dwb.caves.control.character.GameCharacter;

public class Skill extends Observable {

	private GameCharacter owner;

	public GameCharacter getOwner() {
		return owner;
	}

	public void setOwner(GameCharacter owner) {
		this.owner = owner;
	}

	private SkillCost cost;

	public SkillCost getCost() {
		return cost;
	}

	public void setCost(SkillCost cost) {
		this.cost = cost;
	}

	private List<SkillEffect> hitEffects;

	public List<SkillEffect> getHitEffects() {
		return hitEffects;
	}

	public void setHitEffects(List<SkillEffect> effects) {
		hitEffects = effects;
	}

	private List<SkillEffect> missEffects;

	public List<SkillEffect> getMissEffects() {
		return missEffects;
	}

	public void setMissEffects(List<SkillEffect> effects) {
		missEffects = effects;
	}

	private List<SkillEffect> critEffects;

	public List<SkillEffect> getCritEffects() {
		return critEffects;
	}

	public void setCritEffects(List<SkillEffect> effects) {
		critEffects = effects;
	}

	private SkillHitCondition hitCondition;

	public SkillHitCondition getHitCondition() {
		return hitCondition;
	}

	public void setHitCondition(SkillHitCondition condition) {
		hitCondition = condition;
	}

	private SkillTargetSelector targetSelector;
	public SkillTargetSelector getTargetSelector() {
		return targetSelector;
	}

	public void setTargetSelector(SkillTargetSelector selector) {
		targetSelector = selector;
	}

	private String iconName;
	public void setIconName(String newName) {
		iconName = newName;
	}
	public String getIconName() {
		return iconName;
	}

	private String name;
	public void setName(String newName) {
		name = newName;
	}
	public String getName() {
		return name;
	}

	public Skill(String name) {
		super();
		this.name = name;
	}

	public void use() {
		// Initialise some of the data used by other objects.
		SkillContext context = new SkillContext();
		context.put("skill", this);
		context.put("source", owner);
		// The TargetSelector will do its magic, add the targets to the context,
		// then call resolve(context).
		getTargetSelector().pickTargets(context);
	}

	@SuppressWarnings("unchecked")
	public void resolve(SkillContext context) {
		owner.payCost(getCost());
		// "targets" has been set by the TargetSelector. It is always a Set of
		// GameCharacters.
		// It can't be null, as this method wouldn't be called otherwise.
		Set<GameCharacter> targets = (Set<GameCharacter>) context
				.get("targets");
		for (GameCharacter target : targets) {
			// Current target is used by a number of abilities. It is always a
			// GameCharacter.
			// It is also used by the HitCondition to know against whom the
			// condition must be tested.
			context.put("current target", target);
			// The hit condition will dispatch the result on hit(target),
			// miss(target) or critical(target).
			hitCondition.test(context);
		}
	}

	public void miss(SkillContext context) {
		for (SkillEffect effect : missEffects)
			effect.apply(context);
	}

	public void hit(SkillContext context) {
		for (SkillEffect effect : hitEffects)
			effect.apply(context);
	}

	public void critical(SkillContext context) {
		for (SkillEffect effect : critEffects)
			effect.apply(context);
	}

}
