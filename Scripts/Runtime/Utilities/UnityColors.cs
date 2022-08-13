using System;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary> Contains Unity Console built-in colors. </summary>
/// <remarks>
/// All values are from Unity Documentation
/// <br/>https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html
/// <br/>For additional info read comments inside the class.
/// </remarks>
internal static class UnityColors {

	// In Unity documentation they use #RRGGBBAA format,
	// where R - red, G - green, B - blue, a - alpha (transparency),
	// but during my tests I found that <color=value> works when
	// 'value' is any of the following formats:
	// 1. #RRGGBBAA
	// 2. #RRGGBB
	// 4. #RGBA
	// 4. #RGB
	// 5. default color name (22 names, 20 uniq colors)

	public enum UnityColorType {
		White     = 0,
		Silver    = 1,
		Grey      = 2,
		Black     = 3,
		Brown     = 4,
		Maroon    = 5,
		Red       = 6,
		Orange    = 7,
		Yellow    = 8,
		Olive     = 9,
		Lime      = 10,
		Green     = 11,
		Lightblue = 12,
		Cyan      = 13/*Same as Aqua*/,
		Aqua      = 14/*Same as Cyan*/,
		Teal      = 15,
		Blue      = 16,
		Darkblue  = 17,
		Navy      = 18,
		Magenta   = 19/*Same as Fuchsia*/,
		Fuchsia   = 20/*Same as Magenta*/,
		Purple    = 21,
	}

	public static readonly GffColor DefaultLightSkinText = new GffColor(new Color(0.2f, 0.2f, 0.2f),    /*"#020202",*/"#000");
	public static readonly GffColor DefaultDarkSkinText = new GffColor(new Color(0.82f, 0.82f, 0.82f), /*"#d2d2d2",*/"#ddd");

	public struct ColorScheme {
		public static readonly ColorScheme LightSkin =
			new ColorScheme() {
				Text = new GffColor(new Color(0.2f, 0.2f, 0.2f), /*"#020202",*/"#000"),
			};

		public static readonly ColorScheme DarkSkin =
			new ColorScheme() {
				Text         = new GffColor(new Color(0.82f, 0.82f, 0.82f), /*"#d2d2d2",*/"#ddd"),
				Border       = new GffColor(new Color(0.1f,  0.1f,  0.1f), /*"#191919",*/ "#111"),
				ButtonBg     = new GffColor(new Color(0.4f,  0.4f,  0.4f), /*"#676767",*/ "#666"),
				ValueFieldBg = new GffColor(new Color(0.16f, 0.16f, 0.16f), /*"#2a2a2a",*/"#333"),
				_backgrounds = new GffColor[5] {
					new GffColor(new Color(0.22f, 0.22f, 0.22f), /*"#383838",*/"#444"),
					new GffColor(new Color(0.25f, 0.25f, 0.25f), /*"#404040",*/"#444"),
					new GffColor(new Color(0.28f, 0.28f, 0.28f), /*"#474747",*/"#444"),
					new GffColor(new Color(0.3f,  0.3f,  0.3f), /*"#4c4c4c",*/ "#555"),
					new GffColor(new Color(0.31f, 0.31f, 0.31f), /*"#505050",*/"#555"),
				},
			};
		public GffColor Text         { get; private set; }
		public GffColor Border       { get; private set; }
		public GffColor ButtonBg     { get; private set; }
		public GffColor ValueFieldBg { get; private set; }
		public GffColor Bg           => _backgrounds[0];

		private GffColor[] _backgrounds;

		public GffColor GetBgIndentLevel(int indentLevel) {
			indentLevel = Mathf.Max(indentLevel, 0);
			return indentLevel >= _backgrounds.Length ? _backgrounds[^1] : _backgrounds[indentLevel];
		}
	}

	internal static readonly NamedHexColor White     = new NamedHexColor(name: "white"    , hex: "#ffffff", shortHex: "#fff");
	internal static readonly NamedHexColor Silver    = new NamedHexColor(name: "silver"   , hex: "#c0c0c0", shortHex: "#ccc");
	internal static readonly NamedHexColor Grey      = new NamedHexColor(name: "grey"     , hex: "#808080", shortHex: "#888");
	internal static readonly NamedHexColor Black     = new NamedHexColor(name: "black"    , hex: "#000000", shortHex: "#000");
	internal static readonly NamedHexColor Brown     = new NamedHexColor(name: "brown"    , hex: "#a52a2a", shortHex: "#a33");
	internal static readonly NamedHexColor Maroon    = new NamedHexColor(name: "maroon"   , hex: "#800000", shortHex: "#800");
	internal static readonly NamedHexColor Red       = new NamedHexColor(name: "red"      , hex: "#ff0000", shortHex: "#f00");
	internal static readonly NamedHexColor Orange    = new NamedHexColor(name: "orange"   , hex: "#ffa500", shortHex: "#fa0");
	internal static readonly NamedHexColor Yellow    = new NamedHexColor(name: "yellow"   , hex: "#ffff00", shortHex: "#ff0");
	internal static readonly NamedHexColor Olive     = new NamedHexColor(name: "olive"    , hex: "#808000", shortHex: "#880");
	internal static readonly NamedHexColor Lime      = new NamedHexColor(name: "lime"     , hex: "#00ff00", shortHex: "#0f0");
	internal static readonly NamedHexColor Green     = new NamedHexColor(name: "green"    , hex: "#008000", shortHex: "#080");
	internal static readonly NamedHexColor Lightblue = new NamedHexColor(name: "lightblue", hex: "#add8e6", shortHex: "#bce");
	internal static readonly NamedHexColor Cyan      = new NamedHexColor(name: "cyan"     , hex: "#00ffff", shortHex: "#0ff");
	internal static readonly NamedHexColor Aqua      = new NamedHexColor(name: "aqua"     , hex: "#00ffff", shortHex: "#0ff");
	internal static readonly NamedHexColor Teal      = new NamedHexColor(name: "teal"     , hex: "#008080", shortHex: "#088");
	internal static readonly NamedHexColor Blue      = new NamedHexColor(name: "blue"     , hex: "#0000ff", shortHex: "#00f");
	internal static readonly NamedHexColor Darkblue  = new NamedHexColor(name: "darkblue" , hex: "#0000a0", shortHex: "#00a");
	internal static readonly NamedHexColor Navy      = new NamedHexColor(name: "navy"     , hex: "#000080", shortHex: "#008");
	internal static readonly NamedHexColor Magenta   = new NamedHexColor(name: "magenta"  , hex: "#ff00ff", shortHex: "#f0f");
	internal static readonly NamedHexColor Fuchsia   = new NamedHexColor(name: "fuchsia"  , hex: "#ff00ff", shortHex: "#f0f");
	internal static readonly NamedHexColor Purple    = new NamedHexColor(name: "purple"   , hex: "#800080", shortHex: "#808");

	internal static string GetColor(UnityColorType color, NamedHexColor.Format format = NamedHexColor.Format.ShortHexColor) {
		switch (color) {
			case UnityColorType.White:     return White    .GetString(format);
			case UnityColorType.Silver:    return Silver   .GetString(format);
			case UnityColorType.Grey:      return Grey     .GetString(format);
			case UnityColorType.Black:     return Black    .GetString(format);
			case UnityColorType.Brown:     return Brown    .GetString(format);
			case UnityColorType.Maroon:    return Maroon   .GetString(format);
			case UnityColorType.Red:       return Red      .GetString(format);
			case UnityColorType.Orange:    return Orange   .GetString(format);
			case UnityColorType.Yellow:    return Yellow   .GetString(format);
			case UnityColorType.Olive:     return Olive    .GetString(format);
			case UnityColorType.Lime:      return Lime     .GetString(format);
			case UnityColorType.Green:     return Green    .GetString(format);
			case UnityColorType.Lightblue: return Lightblue.GetString(format);
			case UnityColorType.Cyan:      return Cyan     .GetString(format);
			case UnityColorType.Aqua:      return Aqua     .GetString(format);
			case UnityColorType.Teal:      return Teal     .GetString(format);
			case UnityColorType.Blue:      return Blue     .GetString(format);
			case UnityColorType.Darkblue:  return Darkblue .GetString(format);
			case UnityColorType.Navy:      return Navy     .GetString(format);
			case UnityColorType.Magenta:   return Magenta  .GetString(format);
			case UnityColorType.Fuchsia:   return Fuchsia  .GetString(format);
			case UnityColorType.Purple:    return Purple   .GetString(format);
			default: throw new ArgumentOutOfRangeException(nameof(color), color, null);
		}
	}

	internal static bool TryGetColor(string colorName, out NamedHexColor namedHexColor) {
		colorName = colorName.ToLower();
		switch (colorName) {
			case "white":     namedHexColor = White    ; return true;
			case "silver":    namedHexColor = Silver   ; return true;
			case "grey":      namedHexColor = Grey     ; return true;
			case "black":     namedHexColor = Black    ; return true;
			case "brown":     namedHexColor = Brown    ; return true;
			case "maroon":    namedHexColor = Maroon   ; return true;
			case "red":       namedHexColor = Red      ; return true;
			case "orange":    namedHexColor = Orange   ; return true;
			case "yellow":    namedHexColor = Yellow   ; return true;
			case "olive":     namedHexColor = Olive    ; return true;
			case "lime":      namedHexColor = Lime     ; return true;
			case "green":     namedHexColor = Green    ; return true;
			case "lightblue": namedHexColor = Lightblue; return true;
			case "cyan":      namedHexColor = Cyan     ; return true;
			case "aqua":      namedHexColor = Aqua     ; return true;
			case "teal":      namedHexColor = Teal     ; return true;
			case "blue":      namedHexColor = Blue     ; return true;
			case "darkblue":  namedHexColor = Darkblue ; return true;
			case "navy":      namedHexColor = Navy     ; return true;
			case "magenta":   namedHexColor = Magenta  ; return true;
			case "fuchsia":   namedHexColor = Fuchsia  ; return true;
			case "purple":    namedHexColor = Purple   ; return true;
			default:          namedHexColor = White; return false;
		}
	}

	internal readonly struct NamedHexColor {
		internal readonly string Name;
		internal readonly string Hex;
		internal readonly string ShortHex;

		internal NamedHexColor(string name, string hex, string shortHex) {
			Name = name;
			Hex = hex;
			ShortHex = shortHex;
		}

		internal string GetString(Format format) {
			switch (format) {
				case Format.Name:          return Name;
				case Format.HexColor:      return Hex;
				case Format.ShortHexColor: return ShortHex;
				default: throw new ArgumentOutOfRangeException(nameof(format), format, null);
			}
		}

		internal enum Format {
			Name,
			HexColor,
			ShortHexColor
		}
	}
}
}
