package com.dwb.caves.control.skills.values;

import com.dwb.caves.control.skills.SkillContext;

public class MultiplierValueCalculator extends ValueCalculator {

	private ValueCalculator value1, value2;
	
	public MultiplierValueCalculator(ValueCalculator value1,
			ValueCalculator value2) {
		super();
		this.value1 = value1;
		this.value2 = value2;
	}



	public float value(SkillContext context) {
		return value1.value(context)*value2.value(context);
	}

}
