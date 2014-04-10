package combat;

import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.character.statistics.Statistic;
import com.dwb.caves.control.skills.Skill;
import com.dwb.caves.control.skills.SkillContext;
import com.dwb.caves.control.skills.factory.SkillFactory;

public class RepeatedCombatTest {

	/**
	 * @param args
	 * @throws UnimplementedException 
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		long time = System.currentTimeMillis();

		Map<Integer, Integer> stats= new HashMap<Integer, Integer>();

		GameCharacter attacker=GameCharacter.createGameCharacter();
		GameCharacter defender=GameCharacter.createGameCharacter();
		float average=0;
		int minimum=100;
		int maximum=0;
		int atkNumber;
		
		int numberOfTries = 100000;
		
		Skill attack = SkillFactory.initAttack(attacker, defender);

		for(int i=0; i<=numberOfTries; i++){
			defender.setHitPoints((int) defender.getStatisticsManager().getStat(Statistic.MAX_HEALTH).getValue(new SkillContext()));
			atkNumber=0;
			while (defender.getHitPoints()>0){
				atkNumber++;
				attack.use();
			}
			average+=atkNumber;
			if (atkNumber>maximum)
				maximum=atkNumber;
			if (atkNumber<minimum)
				minimum=atkNumber;
			if(stats.get(atkNumber)==null)
				stats.put(atkNumber, 0);
			stats.put(atkNumber, stats.get(atkNumber)+1);
		}
		average/=numberOfTries;

		System.out.println(System.currentTimeMillis()-time);
		
		System.out.println("\n\n\nnb moyen d'attaques : "+average);
		System.out.println("nb minimum d'attaques : "+minimum);
		System.out.println("nb maximum d'attaques : "+maximum);
		for(Entry<Integer,Integer> entry : stats.entrySet())
			System.out.println(entry.getKey()+"	"+entry.getValue());
	}

}
