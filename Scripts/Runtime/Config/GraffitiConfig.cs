using System;
using UnityEngine;

namespace Graffiti {
[Serializable]
public class GraffitiConfig {

	internal static readonly GraffitiConfig DefaultInstance = new GraffitiConfig();

	public bool Disabled => disabled || (disabledInBuild && !Application.isEditor);

    [Header("General")]
    [SerializeField] private bool disabled = false;
    [SerializeField] private bool disabledInBuild = true;

    [Header("Text")]
    public bool ApplySize      = true;
    public bool ApplyColor     = true;
    public bool ApplyGradient  = true;
    public bool ApplyFontStyle = true;

    [Header("Modifier characters (Underline/Strikethrough/etc.)")]
    [Tooltip("If true:\n" +
             "Underline/Strikethrough/etc. will be applied.\n\n" +
             "If false:\n" +
             "Underline/Strikethrough/etc. won't be applied (they will be ignored).")]
    public bool AllowModifierCharacters = true;

	[Tooltip("If true:\n" +
	         "Letters/Words will be able to have multiple modifier characters (Underline/Strikethrough/etc.), \n" +
	         "but not of the same kind (for example, Text won't be able to have Underline and be Dotted).\n" +
	         "Example (Text with Underline and Strikethrough):\n\n" +
	         "\"My Cool Text\".Stylize().Underline.Strikethrough\n")]
	public bool AllowMultipleModifierCharacters = true;

	[Tooltip("If true:\n" +
	         "All Underline/Strikethrough/etc. will be of default text color event if you specified other color to text.\n" +
	         "Example (Text will be Red, but Underline won't):\n\n" +
	         "\"My Cool Text\".Stylize().Red.And().Underline\n")]
	public bool AddDefaultColorToModifierCharacter;

	[Tooltip("If true:\n" +
	         "Modifier characters (Underline/Strikethrough/etc.) will be able to have their own [style]\n" +
	         "Example (Blue Text with Yellow Underline):\n\n" +
	         "\"My Cool Text\".Stylize().Blue.Underline[Style.Yellow]")]
	public bool AllowModifierCharacterStyle = true;
}
}
