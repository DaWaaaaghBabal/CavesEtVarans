using CavesEtVarans.battlefield;
using CavesEtVarans.character;

namespace CavesEtVarans.skills.core {
	public interface ITargetSelector
	{
		void Activate();
        void Activate(ICoordinates center, int centerheight, int suffix);
        void Terminate();

		void TargetCharacter(Character character);
		void TargetTile(Tile tile);
        void Cancel();
    }
}