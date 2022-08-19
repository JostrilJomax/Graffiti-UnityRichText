//--------------------------------------------------------------------------------------
// This file is generated. Modifications to this file won't be saved.
// Last generated: 2022-08-19 23:20:43
// If you want to make any permanent changes to this file, go to the class T4CompilerTest
// At: C:/Home/Projects/Unity/General Purpose Unity Project/Assets/Plugins/Graffiti v0.6/Scripts/Runtime/CodeGen Tests/T4CompilerTest.cs
//--------------------------------------------------------------------------------------

using JetBrains.Annotations;
using UnityEngine;
using Graffiti.Internal;

namespace Graffiti.Internal {
internal enum ColorType {
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

namespace Graffiti {
[PublicAPI] 
public partial class ColorPalette {

	[field: SerializeField] public GffColor White  { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
	[field: SerializeField] public GffColor Grey   { get; private set; } = new GffColor(new Color(0.67f, 0.67f, 0.67f), "#aaa");
	[field: SerializeField] public GffColor Black  { get; private set; } = new GffColor(new Color(0.07f, 0.07f, 0.07f), "#111");
	[field: SerializeField] public GffColor Red    { get; private set; } = new GffColor(new Color(0.93f, 0.23f, 0.25f), "#F45");
	[field: SerializeField] public GffColor Orange { get; private set; } = new GffColor(new Color(0.93f, 0.59f, 0.18f), "#FA3");
	[field: SerializeField] public GffColor Yellow { get; private set; } = new GffColor(new Color(1f,    0.89f, 0.26f), "#FF5");
	[field: SerializeField] public GffColor Green  { get; private set; } = new GffColor(new Color(0.22f,  0.8f,  0.5f), "#4D8");
	[field: SerializeField] public GffColor Blue   { get; private set; } = new GffColor(new Color(0.19f, 0.63f, 0.69f), "#3AB");
	[field: SerializeField] public GffColor Purple { get; private set; } = new GffColor(new Color(0.89f, 0.27f, 0.69f), "#E4B");
	[field: SerializeField] public GffColor Violet { get; private set; } = new GffColor(new Color(0.49f, 0.38f, 0.76f), "#86D");

	internal GffColor FindColor(ColorType color) {
		switch(color) {
			default:
			case ColorType.Default: return DefaultConsoleColor;
			case ColorType.White : return White ;
			case ColorType.Grey  : return Grey  ;
			case ColorType.Black : return Black ;
			case ColorType.Red   : return Red   ;
			case ColorType.Orange: return Orange;
			case ColorType.Yellow: return Yellow;
			case ColorType.Green : return Green ;
			case ColorType.Blue  : return Blue  ;
			case ColorType.Purple: return Purple;
			case ColorType.Violet: return Violet;
		}
	}
}
}

namespace Graffiti {
[PublicAPI] 
public partial class StringStyle : StringStyle.IOnlyColor {

	[PublicAPI] 
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

	public StringStyle DefaultColor => PrepareColor(ColorType.Default);
	public StringStyle White  => PrepareColor(ColorType.White );
	public StringStyle Grey   => PrepareColor(ColorType.Grey  );
	public StringStyle Black  => PrepareColor(ColorType.Black );
	public StringStyle Red    => PrepareColor(ColorType.Red   );
	public StringStyle Orange => PrepareColor(ColorType.Orange);
	public StringStyle Yellow => PrepareColor(ColorType.Yellow);
	public StringStyle Green  => PrepareColor(ColorType.Green );
	public StringStyle Blue   => PrepareColor(ColorType.Blue  );
	public StringStyle Purple => PrepareColor(ColorType.Purple);
	public StringStyle Violet => PrepareColor(ColorType.Violet);

	public StringStyle None              => PrepareModifierCharacter(ModifierCharacterType.None             );
	public StringStyle SmokingHot        => PrepareModifierCharacter(ModifierCharacterType.SmokingHot       );
	public StringStyle Strikethrough     => PrepareModifierCharacter(ModifierCharacterType.Strikethrough    );
	public StringStyle WavyStrikethrough => PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough);
	public StringStyle Slash             => PrepareModifierCharacter(ModifierCharacterType.Slash            );
	public StringStyle HighSlash         => PrepareModifierCharacter(ModifierCharacterType.HighSlash        );
	public StringStyle Underline         => PrepareModifierCharacter(ModifierCharacterType.Underline        );
	public StringStyle DoubleUnderline   => PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  );
	public StringStyle Dotted            => PrepareModifierCharacter(ModifierCharacterType.Dotted           );
	public StringStyle Wheel             => PrepareModifierCharacter(ModifierCharacterType.Wheel            );
}
}

namespace Graffiti {
[PublicAPI] 
public partial class StyledString : StyledString.IOnlyColor {

	[PublicAPI] 
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

	public StyledString DefaultColor { get { LastStyle.PrepareColor(ColorType.Default); return this; } }
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

	public StyledString None              { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.None             ); return this; } }
	public StyledString SmokingHot        { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot       ); return this; } }
	public StyledString Strikethrough     { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Strikethrough    ); return this; } }
	public StyledString WavyStrikethrough { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough); return this; } }
	public StyledString Slash             { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Slash            ); return this; } }
	public StyledString HighSlash         { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.HighSlash        ); return this; } }
	public StyledString Underline         { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Underline        ); return this; } }
	public StyledString DoubleUnderline   { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  ); return this; } }
	public StyledString Dotted            { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Dotted           ); return this; } }
	public StyledString Wheel             { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Wheel            ); return this; } }
}
}

namespace Graffiti {
[PublicAPI] 
public static partial class Style {

	public static StringStyle DefaultColor => StringStyle.Create().PrepareColor(ColorType.Default);
	public static StringStyle White  => StringStyle.Create().PrepareColor(ColorType.White );
	public static StringStyle Grey   => StringStyle.Create().PrepareColor(ColorType.Grey  );
	public static StringStyle Black  => StringStyle.Create().PrepareColor(ColorType.Black );
	public static StringStyle Red    => StringStyle.Create().PrepareColor(ColorType.Red   );
	public static StringStyle Orange => StringStyle.Create().PrepareColor(ColorType.Orange);
	public static StringStyle Yellow => StringStyle.Create().PrepareColor(ColorType.Yellow);
	public static StringStyle Green  => StringStyle.Create().PrepareColor(ColorType.Green );
	public static StringStyle Blue   => StringStyle.Create().PrepareColor(ColorType.Blue  );
	public static StringStyle Purple => StringStyle.Create().PrepareColor(ColorType.Purple);
	public static StringStyle Violet => StringStyle.Create().PrepareColor(ColorType.Violet);

	public static StringStyle None              => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.None             );
	public static StringStyle SmokingHot        => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot       );
	public static StringStyle Strikethrough     => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Strikethrough    );
	public static StringStyle WavyStrikethrough => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough);
	public static StringStyle Slash             => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Slash            );
	public static StringStyle HighSlash         => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.HighSlash        );
	public static StringStyle Underline         => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Underline        );
	public static StringStyle DoubleUnderline   => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  );
	public static StringStyle Dotted            => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Dotted           );
	public static StringStyle Wheel             => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Wheel            );
}
}

