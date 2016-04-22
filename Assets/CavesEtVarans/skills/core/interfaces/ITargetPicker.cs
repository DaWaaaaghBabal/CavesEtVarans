using CavesEtVarans.battlefield;
using CavesEtVarans.character;

namespace CavesEtVarans.skills.core {
	public interface ITargetSelector
	{
		void Activate(Context context);
		void TargetCharacter(Character character);
		void TargetTile(Tile tile);
        void Cancel();
    }
}