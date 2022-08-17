using System;
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
	internal char[] Render() {

		char[] sb = { GraffitiStylist.ModifierCharacter.GetModifierCharacter(Type) };

		if (Style == null)
			return sb;

		if (Style.HasOnlyOneColor)
			sb = GraffitiStylist.AddTag.Color(sb, Style.Color.GetColorHexValue());
		else if (Style.HasNoColor && GraffitiProperties.Config.AddDefaultColorToModifierCharacter)
			sb = GraffitiStylist.AddTag.Color(sb, ColorPalette.DefaultConsoleColor.GetHexValue());

		if (Style.HasSize)
			sb = GraffitiStylist.AddTag.Size(sb, Style.SizeValue);

		return sb;
	}
}
}
