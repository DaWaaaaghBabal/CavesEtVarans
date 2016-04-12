using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.exceptions;
using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.skills.target {

	/// <summary>
	/// Validating targets comes in three flavours :
	///	1/ No validation : targets are selected automatically and the skill is used without confirmation.
	/// 2/ User validation : targets are selected automatically, but the player can confirm or cancel.
	/// 3/ User choice : targets are chosen by the player.
	/// </summary>
	public enum PlayerChoiceType {
		NoValidation, PlayerValidation, PlayerChoice
	}

	public delegate bool TargetPickerCallback(Tile t);

	public abstract class PlayerChoiceStrategy : ContextDependent {

		protected TargetPickerCallback Callback;
		protected PlayerChoiceStrategy(TargetPickerCallback callback) {
			Callback = callback;
		}

		public static PlayerChoiceStrategy GetStrategy(PlayerChoiceType value, TargetPickerCallback callback) {
			switch (value) {
				case PlayerChoiceType.NoValidation:
					return new NoValidation(callback);
				case PlayerChoiceType.PlayerValidation:
					return new PlayerValidation(callback);
				case PlayerChoiceType.PlayerChoice:
					return new PlayerChoice(callback);
			}
			throw new TargetPickerParameterException("The player choice type is incorrect.");
		}

		public abstract PlayerChoiceType ChoiceType();
		/// <summary>
		/// If the character is in range and meets the targeting criteria, targets her or the tile she is standing on (depending on TargetPicker parameters).
		/// </summary>
		/// <returns>true if something was selected, false otherwise (out of range or other targeting criteria)</returns>
		public abstract bool TargetCharacter(Character character);

		/// <summary>
		/// If the tile is in range and meets the targeting criteria, targets it or the character standing on it (depending on TargetPicker parameters).
		/// </summary>
		/// <returns>true if something was selected, false otherwise (out of range or other targeting criteria)</returns>
		public abstract bool TargetTile(Tile tile);
		public abstract void Activate(TargetPicker targetPicker, Context context);

	}
}
