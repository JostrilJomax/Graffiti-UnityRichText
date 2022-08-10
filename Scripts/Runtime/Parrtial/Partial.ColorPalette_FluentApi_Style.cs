// TODO: The name of this file is confusing.

// TODO: This file should be generated, but I had a lot of problems with Microsoft T4 Text Templates.
// Until I understand how to use T4 with unity, I will manually update this file.
// The only reason to make this file generated is to have an easy way to add/remove colors and font styles.

using Graffiti.Internal;
using UnityEngine;

namespace Graffiti {
public enum ColorType {
	Undefined,

	Default,

	White,
	Grey,
	Black,

	Red,
	Orange,
	Yellow,
	Green,
	Blue,
	Purple,
	Violet,
}
}

internal enum UnityBuildInFontStyleType {
	None       = 0,
	Bold       = 1,
	Italic     = 2,
	BoldItalic = 3,
}

namespace Graffiti {
// TODO: Do not swap "Top", "Middle", "Bottom" sections.
public enum ModifierCharacterType {
	None,
	SmokingHot, // Top
	Strikethrough, // Middle
	WavyStrikethrough,
	Slash,
	HighSlash,
	Underline, // Bottom
	DoubleUnderline,
	Dotted,
	Wheel,
}
}

namespace Graffiti {
internal static class ModifierCharacterSplitter {
	internal const int STARTS_CHAR_MODIFIERS_TOP = (int) ModifierCharacterType.SmokingHot;
	internal const int STARTS_CHAR_MODIFIERS_MID = (int) ModifierCharacterType.Strikethrough;
	internal const int STARTS_CHAR_MODIFIERS_BOT = (int) ModifierCharacterType.Underline;
}
}

namespace Graffiti {
public partial class StyledString : StyledString.IOnlyColor {

	public interface IOnlyColor {
		public StyledString White  { get; }
		public StyledString Grey   { get; }
		public StyledString Black  { get; }
		public StyledString Red    { get; }
		public StyledString Orange { get; }
		public StyledString Yellow { get; }
		public StyledString Green  { get; }
		public StyledString Blue   { get; }
		public StyledString Purple { get; }
		public StyledString Violet { get; }
	}

	public StyledString White  { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
	public StyledString Grey   { get { LastStyle.PrepareColor(ColorType.Grey  ); return this; } }
	public StyledString Black  { get { LastStyle.PrepareColor(ColorType.Black ); return this; } }
	public StyledString Red    { get { LastStyle.PrepareColor(ColorType.Red   ); return this; } }
	public StyledString Orange { get { LastStyle.PrepareColor(ColorType.Orange); return this; } }
	public StyledString Yellow { get { LastStyle.PrepareColor(ColorType.Yellow); return this; } }
	public StyledString Green  { get { LastStyle.PrepareColor(ColorType.Green ); return this; } }
	public StyledString Blue   { get { LastStyle.PrepareColor(ColorType.Blue  ); return this; } }
	public StyledString Purple { get { LastStyle.PrepareColor(ColorType.Purple); return this; } }
	public StyledString Violet { get { LastStyle.PrepareColor(ColorType.Violet); return this; } }

	public StyledString SmokingHot        { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot       ); return this; } }
	public StyledString Strikethrough     { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Strikethrough    ); return this; } }
	public StyledString WavyStrikethrough { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough); return this; } }
	public StyledString Slash             { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Slash            ); return this; } }
	public StyledString HighSlash         { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.HighSlash        ); return this; } }
	public StyledString Underline         { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Underline        ); return this; } }
	public StyledString DoubleUnderline   { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  ); return this; } }
	public StyledString Dotted            { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Dotted           ); return this; } }
	public StyledString Wheel             { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Wheel            ); return this; } }

	public IOnlyColor   Dark  { get { LastStyle.PrepareColorModification(GffColor.Modifier.Dark);  return this; } }
	public IOnlyColor   Light { get { LastStyle.PrepareColorModification(GffColor.Modifier.Light); return this; } }
	public StyledString Bold   { get { LastStyle.PrepareFontStyle(UnityBuildInFontStyleType.Bold  ); return this; } }
	public StyledString Italic { get { LastStyle.PrepareFontStyle(UnityBuildInFontStyleType.Italic); return this; } }
	public StyledString DefaultColor { get { LastStyle.PrepareColor(ColorType.Default ); return this; } }
	public StyledString Size(int size) { LastStyle.PrepareSize(size); return this; }
}
}

namespace Graffiti {
public partial class ColorPalette {

	[field: SerializeField] public GffColor White  { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f) /*"#eeeeee"*/, "#eee");
	[field: SerializeField] public GffColor Grey   { get; private set; } = new GffColor(new Color(0.67f, 0.67f, 0.67f) /*"#aaaaaa"*/, "#aaa");
	[field: SerializeField] public GffColor Black  { get; private set; } = new GffColor(new Color(0.07f, 0.07f, 0.07f) /*"#111111"*/, "#111");
	[field: SerializeField] public GffColor Red    { get; private set; } = new GffColor(new Color(0.93f, 0.23f, 0.25f) /*"#EC3A41"*/, "#F45");
	[field: SerializeField] public GffColor Orange { get; private set; } = new GffColor(new Color(0.93f, 0.59f, 0.18f) /*"#ED962F"*/, "#FA3");
	[field: SerializeField] public GffColor Yellow { get; private set; } = new GffColor(new Color(1f, 0.89f, 0.26f)    /*"#FFE443"*/, "#FF5");
	[field: SerializeField] public GffColor Green  { get; private set; } = new GffColor(new Color(0.22f, 0.8f, 0.5f)   /*"#39CD7F"*/, "#4D8");
	[field: SerializeField] public GffColor Blue   { get; private set; } = new GffColor(new Color(0.19f, 0.63f, 0.69f) /*"#30a0b0"*/, "#3AB");
	[field: SerializeField] public GffColor Purple { get; private set; } = new GffColor(new Color(0.89f, 0.27f, 0.69f) /*"#E245B0"*/, "#E4B");
	[field: SerializeField] public GffColor Violet { get; private set; } = new GffColor(new Color(0.49f, 0.38f, 0.76f) /*"#7d60c3"*/, "#86D");

	public GffColor FindColors(ColorType color) {
		switch (color) {
			default:
			case ColorType.Default: return DefaultConsoleColors;
			case ColorType.White:   return White ;
			case ColorType.Grey:    return Grey  ;
			case ColorType.Black:   return Black ;
			case ColorType.Red:     return Red   ;
			case ColorType.Orange:  return Orange;
			case ColorType.Yellow:  return Yellow;
			case ColorType.Green:   return Green ;
			case ColorType.Blue:    return Blue  ;
			case ColorType.Purple:  return Purple;
			case ColorType.Violet:  return Violet;
		}
	}
}
}

namespace Graffiti {
public static partial class Style {

	public static StringStyle White  { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.White ); return stl; } }
	public static StringStyle Grey   { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Grey  ); return stl; } }
	public static StringStyle Black  { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Black ); return stl; } }
	public static StringStyle Red    { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Red   ); return stl; } }
	public static StringStyle Orange { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Orange); return stl; } }
	public static StringStyle Yellow { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Yellow); return stl; } }
	public static StringStyle Green  { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Green ); return stl; } }
	public static StringStyle Blue   { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Blue  ); return stl; } }
	public static StringStyle Purple { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Purple); return stl; } }
	public static StringStyle Violet { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Violet); return stl; } }

	public static StringStyle SmokingHot        { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.SmokingHot        ); return stl; } }
	public static StringStyle Strikethrough     { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.Strikethrough     ); return stl; } }
	public static StringStyle WavyStrikethrough { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough ); return stl; } }
	public static StringStyle Slash             { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.Slash             ); return stl; } }
	public static StringStyle HighSlash         { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.HighSlash         ); return stl; } }
	public static StringStyle Underline         { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.Underline         ); return stl; } }
	public static StringStyle DoubleUnderline   { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline   ); return stl; } }
	public static StringStyle Dotted            { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.Dotted            ); return stl; } }
	public static StringStyle Wheel             { get { var stl = new StringStyle(); stl.PrepareModifierCharacter(ModifierCharacterType.Wheel             ); return stl; } }

	public static StringStyle.IOnlyColor Dark  { get { var stl = new StringStyle(); stl.PrepareColorModification(GffColor.Modifier.Dark);  return stl; } }
	public static StringStyle.IOnlyColor Light { get { var stl = new StringStyle(); stl.PrepareColorModification(GffColor.Modifier.Light); return stl; } }
	// public StringStyle Bold   { get { PrepareFontStyle(UnityBuildInFontStyleType.Bold  ); return this; } }
	// public StringStyle Italic { get { PrepareFontStyle(UnityBuildInFontStyleType.Italic); return this; } }
	public static StringStyle DefaultColor   { get { var stl = new StringStyle(); stl.PrepareColor(ColorType.Default ); return stl; } }
	public static StringStyle Size(int size) { var stl = new StringStyle(); stl.PrepareSize(size); return stl; }
}
}

namespace Graffiti {
public partial class StringStyle : StringStyle.IOnlyColor {

	public interface IOnlyColor {
		public StringStyle White  { get; }
		public StringStyle Grey   { get; }
		public StringStyle Black  { get; }
		public StringStyle Red    { get; }
		public StringStyle Orange { get; }
		public StringStyle Yellow { get; }
		public StringStyle Green  { get; }
		public StringStyle Blue   { get; }
		public StringStyle Purple { get; }
		public StringStyle Violet { get; }
	}

	public StringStyle White  { get { PrepareColor(ColorType.White ); return this; } }
	public StringStyle Grey   { get { PrepareColor(ColorType.Grey  ); return this; } }
	public StringStyle Black  { get { PrepareColor(ColorType.Black ); return this; } }
	public StringStyle Red    { get { PrepareColor(ColorType.Red   ); return this; } }
	public StringStyle Orange { get { PrepareColor(ColorType.Orange); return this; } }
	public StringStyle Yellow { get { PrepareColor(ColorType.Yellow); return this; } }
	public StringStyle Green  { get { PrepareColor(ColorType.Green ); return this; } }
	public StringStyle Blue   { get { PrepareColor(ColorType.Blue  ); return this; } }
	public StringStyle Purple { get { PrepareColor(ColorType.Purple); return this; } }
	public StringStyle Violet { get { PrepareColor(ColorType.Violet); return this; } }

	public StringStyle SmokingHot        { get { PrepareModifierCharacter(ModifierCharacterType.SmokingHot        ); return this; } }
	public StringStyle Strikethrough     { get { PrepareModifierCharacter(ModifierCharacterType.Strikethrough     ); return this; } }
	public StringStyle WavyStrikethrough { get { PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough ); return this; } }
	public StringStyle Slash             { get { PrepareModifierCharacter(ModifierCharacterType.Slash             ); return this; } }
	public StringStyle HighSlash         { get { PrepareModifierCharacter(ModifierCharacterType.HighSlash         ); return this; } }
	public StringStyle Underline         { get { PrepareModifierCharacter(ModifierCharacterType.Underline         ); return this; } }
	public StringStyle DoubleUnderline   { get { PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline   ); return this; } }
	public StringStyle Dotted            { get { PrepareModifierCharacter(ModifierCharacterType.Dotted            ); return this; } }
	public StringStyle Wheel             { get { PrepareModifierCharacter(ModifierCharacterType.Wheel             ); return this; } }

	public IOnlyColor   Dark  { get { PrepareColorModification(GffColor.Modifier.Dark);  return this; } }
	public IOnlyColor   Light { get { PrepareColorModification(GffColor.Modifier.Light); return this; } }
	// public StringStyle Bold   { get { PrepareFontStyle(UnityBuildInFontStyleType.Bold  ); return this; } }
	// public StringStyle Italic { get { PrepareFontStyle(UnityBuildInFontStyleType.Italic); return this; } }
	public StringStyle DefaultColor { get { PrepareColor(ColorType.Default ); return this; } }
	public StringStyle Size(int size) { PrepareSize(size); return this; }
}
}
