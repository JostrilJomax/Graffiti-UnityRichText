using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.IonicZip;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary>
/// This class allows you to add a <b>red underline</b> to your text, for example.
/// </summary>
[Serializable]
internal class ModifierCharacterSet {

	internal bool HasAnydModifierCharacter { get; private set; }
	internal List<ModifierCharacter> ModifierCharacters { get; } = new List<ModifierCharacter>();


	internal void SetModifierCharacter(ModifierCharacterType type) {
		if (type == ModifierCharacterType.None)
			return;
		if (!GraffitiConfigSo.Config.AllowMultipleModifierCharacters && HasAnydModifierCharacter)
			return;

		ModifierCharacters.Add(new ModifierCharacter { Type = type });
		HasAnydModifierCharacter = true;
	}

	public void SetStyleToLastAddedModifierCharacter(StringStyleCore style) =>
		ModifierCharacters[^1].Style = style;

	internal (char[], Gradient)[] Unpack() {
		var pairs = new (char[], Gradient)[ModifierCharacters.Count];

		for (int i = 0; i < ModifierCharacters.Count; i++) {
			char charModifier = GraffitiStylist.ModifierCharacter.GetModifierCharacter(ModifierCharacters[i].Type);
			pairs[i] =
				(ModifierCharacter.Render(charModifier, ModifierCharacters[i].Style),
				ModifierCharacters[i].Style?.Gradient);
		}
		return pairs;
	}
}
}
