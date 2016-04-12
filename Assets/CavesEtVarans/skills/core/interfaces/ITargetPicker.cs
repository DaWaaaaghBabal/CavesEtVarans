using CavesEtVarans.battlefield;
using CavesEtVarans.character;

namespace CavesEtVarans.skills.core {
	public interface ITargetPicker
	{
		void Activate(Context context);
		void TargetCharacter(Character character);
		void TargetTile(Tile tile);
        void Cancel();
    }
}