using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Graffiti.Internal {
[Serializable]
internal class ModifierCharacter {

	public ModifierCharacterType Type;
	[CanBeNull] public StringStyleCore Style;

	/// <remarks>
	/// This method applies the style (if has any) to the modifier character.
	/// <para/>
	/// So, for example, in this method bold/italic is not applied to the modifier character, which
	/// means that the modifier character can't be Bold or Italic.
	/// But this method contains the application of a size and color modifiers, which means that
	/// size and color of the modifier character can be set.
	/// <para/>
	/// • Note that gradient is not applied in this method.
	/// </remarks>
	internal static char[] Render(char self, [CanBeNull] StringStyleCore style) {
		char[] sb = {self};

		if (style == null)
			return sb;

		if (style.HasOnlyOneColor)
			sb = GraffitiStylist.AddTag.Color(sb, style.Color.GetColorHexValue());
		else if (style.HasNoColor && GraffitiConfigSo.Config.AddDefaultColorToModifierCharacter)
			sb = GraffitiStylist.AddTag.Color(sb, ColorPalette.DefaultConsoleColors.Value.GetHexValue());

		if (style.HasSize)
			sb = GraffitiStylist.AddTag.Size(sb, style.SizeValue);

		return sb;
	}
}
}
