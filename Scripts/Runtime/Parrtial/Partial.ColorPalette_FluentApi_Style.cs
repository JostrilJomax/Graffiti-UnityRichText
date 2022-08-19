// // TODO: The name of this file is confusing.
//
// // TODO: This file should be generated, but I had a lot of problems with Microsoft T4 Text Templates.
// // Until I understand how to use T4 with unity, I will manually update this file.
// // The only reason to make this file generated is to have an easy way to add/remove colors and font styles.
//
// using JetBrains.Annotations;
// using UnityEngine;
// using Graffiti.Internal;
//
// namespace Graffiti {
// internal enum ColorType {
// 	Default,
//
// 	White,
// 	Grey,
// 	Black,
//
// 	Red,
// 	Orange,
// 	Yellow,
// 	Green,
// 	Blue,
// 	Purple,
// 	Violet,
// }
// }
//
// namespace Graffiti {
// public partial class ColorPalette {
//
// 	[field: SerializeField] public GffColor White  { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f) /*"#eeeeee"*/, "#eee");
// 	[field: SerializeField] public GffColor Grey   { get; private set; } = new GffColor(new Color(0.67f, 0.67f, 0.67f) /*"#aaaaaa"*/, "#aaa");
// 	[field: SerializeField] public GffColor Black  { get; private set; } = new GffColor(new Color(0.07f, 0.07f, 0.07f) /*"#111111"*/, "#111");
// 	[field: SerializeField] public GffColor Red    { get; private set; } = new GffColor(new Color(0.93f, 0.23f, 0.25f) /*"#EC3A41"*/, "#F45");
// 	[field: SerializeField] public GffColor Orange { get; private set; } = new GffColor(new Color(0.93f, 0.59f, 0.18f) /*"#ED962F"*/, "#FA3");
// 	[field: SerializeField] public GffColor Yellow { get; private set; } = new GffColor(new Color(1f, 0.89f, 0.26f)    /*"#FFE443"*/, "#FF5");
// 	[field: SerializeField] public GffColor Green  { get; private set; } = new GffColor(new Color(0.22f, 0.8f, 0.5f)   /*"#39CD7F"*/, "#4D8");
// 	[field: SerializeField] public GffColor Blue   { get; private set; } = new GffColor(new Color(0.19f, 0.63f, 0.69f) /*"#30a0b0"*/, "#3AB");
// 	[field: SerializeField] public GffColor Purple { get; private set; } = new GffColor(new Color(0.89f, 0.27f, 0.69f) /*"#E245B0"*/, "#E4B");
// 	[field: SerializeField] public GffColor Violet { get; private set; } = new GffColor(new Color(0.49f, 0.38f, 0.76f) /*"#7d60c3"*/, "#86D");
//
// 	internal GffColor FindColor(ColorType color) {
// 		switch (color) {
// 			default:
// 			case ColorType.Default: return DefaultConsoleColor;
// 			case ColorType.White:   return White ;
// 			case ColorType.Grey:    return Grey  ;
// 			case ColorType.Black:   return Black ;
// 			case ColorType.Red:     return Red   ;
// 			case ColorType.Orange:  return Orange;
// 			case ColorType.Yellow:  return Yellow;
// 			case ColorType.Green:   return Green ;
// 			case ColorType.Blue:    return Blue  ;
// 			case ColorType.Purple:  return Purple;
// 			case ColorType.Violet:  return Violet;
// 		}
// 	}
// }
// }
//
// namespace Graffiti {
// public partial class StringStyle : StringStyle.IOnlyColor {
//
// 	public interface IOnlyColor {
// 		public StringStyle White  { get; }
// 		public StringStyle Grey   { get; }
// 		public StringStyle Black  { get; }
// 		public StringStyle Red    { get; }
// 		public StringStyle Orange { get; }
// 		public StringStyle Yellow { get; }
// 		public StringStyle Green  { get; }
// 		public StringStyle Blue   { get; }
// 		public StringStyle Purple { get; }
// 		public StringStyle Violet { get; }
// 	}
//
// 	public StringStyle DefaultColor => PrepareColor(ColorType.Default);
// 	public StringStyle White        => PrepareColor(ColorType.White );
// 	public StringStyle Grey         => PrepareColor(ColorType.Grey  );
// 	public StringStyle Black        => PrepareColor(ColorType.Black );
// 	public StringStyle Red          => PrepareColor(ColorType.Red   );
// 	public StringStyle Orange       => PrepareColor(ColorType.Orange);
// 	public StringStyle Yellow       => PrepareColor(ColorType.Yellow);
// 	public StringStyle Green        => PrepareColor(ColorType.Green );
// 	public StringStyle Blue         => PrepareColor(ColorType.Blue  );
// 	public StringStyle Purple       => PrepareColor(ColorType.Purple);
// 	public StringStyle Violet       => PrepareColor(ColorType.Violet);
//
// 	public StringStyle SmokingHot        => PrepareModifierCharacter(ModifierCharacterType.SmokingHot       );
// 	public StringStyle Strikethrough     => PrepareModifierCharacter(ModifierCharacterType.Strikethrough    );
// 	public StringStyle WavyStrikethrough => PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough);
// 	public StringStyle Slash             => PrepareModifierCharacter(ModifierCharacterType.Slash            );
// 	public StringStyle HighSlash         => PrepareModifierCharacter(ModifierCharacterType.HighSlash        );
// 	public StringStyle Underline         => PrepareModifierCharacter(ModifierCharacterType.Underline        );
// 	public StringStyle DoubleUnderline   => PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  );
// 	public StringStyle Dotted            => PrepareModifierCharacter(ModifierCharacterType.Dotted           );
// 	public StringStyle Wheel             => PrepareModifierCharacter(ModifierCharacterType.Wheel            );
// }
// }
//
// namespace Graffiti {
// public partial class StyledString : StyledString.IOnlyColor {
//
// 	public interface IOnlyColor {
// 		public StyledString White  { get; }
// 		public StyledString Grey   { get; }
// 		public StyledString Black  { get; }
// 		public StyledString Red    { get; }
// 		public StyledString Orange { get; }
// 		public StyledString Yellow { get; }
// 		public StyledString Green  { get; }
// 		public StyledString Blue   { get; }
// 		public StyledString Purple { get; }
// 		public StyledString Violet { get; }
// 	}
//
// 	[PublicAPI] public StyledString DefaultColor      { get { LastStyle.PrepareColor(ColorType.Default ); return this; } }
// 	[PublicAPI] public StyledString White             { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
// 	[PublicAPI] public StyledString Grey              { get { LastStyle.PrepareColor(ColorType.Grey  ); return this; } }
// 	[PublicAPI] public StyledString Black             { get { LastStyle.PrepareColor(ColorType.Black ); return this; } }
// 	[PublicAPI] public StyledString Red               { get { LastStyle.PrepareColor(ColorType.Red   ); return this; } }
// 	[PublicAPI] public StyledString Orange            { get { LastStyle.PrepareColor(ColorType.Orange); return this; } }
// 	[PublicAPI] public StyledString Yellow            { get { LastStyle.PrepareColor(ColorType.Yellow); return this; } }
// 	[PublicAPI] public StyledString Green             { get { LastStyle.PrepareColor(ColorType.Green ); return this; } }
// 	[PublicAPI] public StyledString Blue              { get { LastStyle.PrepareColor(ColorType.Blue  ); return this; } }
// 	[PublicAPI] public StyledString Purple            { get { LastStyle.PrepareColor(ColorType.Purple); return this; } }
// 	[PublicAPI] public StyledString Violet            { get { LastStyle.PrepareColor(ColorType.Violet); return this; } }
//
// 	[PublicAPI] public StyledString SmokingHot        { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot       ); return this; } }
// 	[PublicAPI] public StyledString Strikethrough     { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Strikethrough    ); return this; } }
// 	[PublicAPI] public StyledString WavyStrikethrough { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough); return this; } }
// 	[PublicAPI] public StyledString Slash             { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Slash            ); return this; } }
// 	[PublicAPI] public StyledString HighSlash         { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.HighSlash        ); return this; } }
// 	[PublicAPI] public StyledString Underline         { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Underline        ); return this; } }
// 	[PublicAPI] public StyledString DoubleUnderline   { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  ); return this; } }
// 	[PublicAPI] public StyledString Dotted            { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Dotted           ); return this; } }
// 	[PublicAPI] public StyledString Wheel             { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.Wheel            ); return this; } }
// }
// }
//
// namespace Graffiti {
// public static partial class Style {
//
// 	[PublicAPI] public static StringStyle DefaultColor      => StringStyle.Create().PrepareColor(ColorType.Default );
// 	[PublicAPI] public static StringStyle White             => StringStyle.Create().PrepareColor(ColorType.White );
// 	[PublicAPI] public static StringStyle Grey              => StringStyle.Create().PrepareColor(ColorType.Grey  );
// 	[PublicAPI] public static StringStyle Black             => StringStyle.Create().PrepareColor(ColorType.Black );
// 	[PublicAPI] public static StringStyle Red               => StringStyle.Create().PrepareColor(ColorType.Red   );
// 	[PublicAPI] public static StringStyle Orange            => StringStyle.Create().PrepareColor(ColorType.Orange);
// 	[PublicAPI] public static StringStyle Yellow            => StringStyle.Create().PrepareColor(ColorType.Yellow);
// 	[PublicAPI] public static StringStyle Green             => StringStyle.Create().PrepareColor(ColorType.Green );
// 	[PublicAPI] public static StringStyle Blue              => StringStyle.Create().PrepareColor(ColorType.Blue  );
// 	[PublicAPI] public static StringStyle Purple            => StringStyle.Create().PrepareColor(ColorType.Purple);
// 	[PublicAPI] public static StringStyle Violet            => StringStyle.Create().PrepareColor(ColorType.Violet);
//
// 	[PublicAPI] public static StringStyle SmokingHot        => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot        );
// 	[PublicAPI] public static StringStyle Strikethrough     => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Strikethrough     );
// 	[PublicAPI] public static StringStyle WavyStrikethrough => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough );
// 	[PublicAPI] public static StringStyle Slash             => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Slash             );
// 	[PublicAPI] public static StringStyle HighSlash         => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.HighSlash         );
// 	[PublicAPI] public static StringStyle Underline         => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Underline         );
// 	[PublicAPI] public static StringStyle DoubleUnderline   => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline   );
// 	[PublicAPI] public static StringStyle Dotted            => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Dotted            );
// 	[PublicAPI] public static StringStyle Wheel             => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Wheel             );
// }
// }
