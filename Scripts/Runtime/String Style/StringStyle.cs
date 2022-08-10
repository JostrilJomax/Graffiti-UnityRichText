using System;
using Graffiti.Internal;
using UnityEngine;
using UnityEngine.Serialization;

namespace Graffiti {
/// <summary>
/// This class is a style, that <b>contains modifiers</b> (Color/Gradient/Size/etc.) for text
/// + it <b>contains nested styles</b> for modifier characters (Underline/Strikethrough/etc.).
/// </summary>
/// <remarks>
/// In other words, this class allows you to change style of your text + change style of modifier characters (Underline/Strikethrough/etc.).
/// <br/> So, for example, <b>you can make Blue text that is underlined by Bold Yellow line.</b>
/// </remarks>
[Serializable]
public partial class StringStyle : StringStyleCore {

	public override bool IsEmpty =>
		IsScopeEmpty ||
		IsEmpty_WithoutScope && (_modifierCharSet == null || !_modifierCharSet.HasAnyModifierCharacter);

	/// <summary>
	/// Modifier character is a special character that modifies the character that precedes it.
	/// For example, there are such modifier characters as: <b>Underline, Strikethrough</b>, etc.
	/// </summary>
	/// <remarks>
	/// Modifier characters can be combined, so you can have underline <b>and</b> strikethrough modification for the same character.
	/// </remarks>
	internal bool ModifierCharacterExists { get; private set; }
	internal bool HasModifierCharacterSet => _modifierCharSet != null;
	internal ModifierCharacterSet ModifierCharacterSet => _modifierCharSet ??= new ModifierCharacterSet();

	[SerializeField] private ModifierCharacterSet _modifierCharSet;

	internal override void PrepareSize(int size) {
		if (!GraffitiConfigSo.Config.ApplySize) return;
		base.PrepareSize(size);
	}

	internal override void PrepareFontStyle(UnityBuildInFontStyleType fontStyle) {
		if (!GraffitiConfigSo.Config.ApplyFontStyle) return;
		base.PrepareFontStyle(fontStyle);
	}

	internal override void __PrepareColor(ColorType gffColor, string strColor) {
		if (!GraffitiConfigSo.Config.ApplyColor) return;
		base.__PrepareColor(gffColor, strColor);
	}

	internal override void PrepareColorAdditionally(ColorType gffColor, string strColor) {
		if (!GraffitiConfigSo.Config.ApplyGradient) return;
		base.PrepareColorAdditionally(gffColor, strColor);
	}

	internal override void PrepareGradient(Gradient gradient) {
		if (!GraffitiConfigSo.Config.ApplyGradient) return;
		base.PrepareGradient(gradient);
	}

	internal void PrepareModifierCharacter(ModifierCharacterType type) {
		ModifierCharacterExists = type != ModifierCharacterType.None;
		if (!ModifierCharacterExists) return;
		if (!GraffitiConfigSo.Config.AllowModifierCharacters) return;
		ModifierCharacterSet.SetModifierCharacter(type);
	}

	internal void SetStyleTodModifierCharacter(StringStyleCore style) =>
		ModifierCharacterSet.SetStyleToLastAddedModifierCharacter(style);
}
}
