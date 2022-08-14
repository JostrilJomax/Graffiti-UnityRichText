using System;
using System.Diagnostics.CodeAnalysis;
using Graffiti.Internal;

namespace Graffiti {
/// <summary> Partial class that contains pre-written colors. New colors can only be added through code. </summary>
/// <remarks> This class is partial because one part of it (colors) should be generated. </remarks>
[Serializable]
public partial class ColorPalette {

	/// <summary> Default color palette. Is used when no custom palette is defined in config. </summary>
 	[NotNull]
	internal static readonly ColorPalette DefaultInstance = new ColorPalette();

	/// <summary> Color of text in Unity Console, depends on current Unity Editor skin (Dark/Light) </summary>
	internal static GffColor DefaultConsoleColors => UnitySettingsUtility.IsDarkSkin ? DefaultDark : DefaultLight;

	private static readonly GffColor DefaultDark  = new GffColor(UnityColors.DefaultDarkSkinText);
	private static readonly GffColor DefaultLight = new GffColor(UnityColors.DefaultLightSkinText);


	internal string GetColorHexValue(ColorType color) => FindColors(color).GetHexValue();
}
}
