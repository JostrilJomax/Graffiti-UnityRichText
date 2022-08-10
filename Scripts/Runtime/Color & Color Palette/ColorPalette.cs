﻿using System;
using Graffiti.Internal;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Graffiti {
/// <summary> Partial class that contains pre-written colors. New colors can only be added through code. </summary>
/// <remarks> This class is partial because one part of it (colors) should be generated. </remarks>
[Serializable]
public partial class ColorPalette {

	/// <summary> Default color palette. Is used when no custom palette is defined in config. </summary>
	public static readonly ColorPalette DefaultInstance = new ColorPalette();

	public string GetColorHexValue(ColorType color) => FindColorsSet(color).GetHexValue();

	/// <summary> Color of text in Unity Console, depends on current Unity Editor skin (Dark/Light) </summary>
	public static GffColor DefaultConsoleColors => UnitySettingsUtility.IsDarkSkin ? DefaultDark : DefaultLight;

	private static readonly GffColor DefaultLight = new GffColor(UnityConsoleColors.DefaultLight);
	private static readonly GffColor DefaultDark  = new GffColor(UnityConsoleColors.DefaultDark);
}
}
