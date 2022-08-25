// //--------------------------------------------------------------------------------------
// // This file is generated. Modifications to this file won't be saved.
// // Last generated: 2022-08-20 16:33:04
// // If you want to make any permanent changes to this file, go to the class T4CompilerTest
// // At: C:/Home/Projects/Unity/General Purpose Unity Project/Assets/Plugins/Graffiti v0.6/Scripts/Runtime/CodeGen Tests/T4CompilerTest.cs
// //--------------------------------------------------------------------------------------
//
// using JetBrains.Annotations;
// using UnityEngine;
// using Graffiti.Internal;
// using Graffiti.Internal.Helpers;
//
// namespace Graffiti.Internal {
// internal enum ColorType {
// 	Default,
// 	White,
// 	Grey,
// 	Black,
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
// [PublicAPI]
// public partial class ColorPalette {
//
// 	[field: SerializeField] public GffColor White  { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
// 	[field: SerializeField] public GffColor Grey   { get; private set; } = new GffColor(new Color(0.67f, 0.67f, 0.67f), "#aaa");
// 	[field: SerializeField] public GffColor Black  { get; private set; } = new GffColor(new Color(0.07f, 0.07f, 0.07f), "#111");
// 	[field: SerializeField] public GffColor Red    { get; private set; } = new GffColor(new Color(0.93f, 0.23f, 0.25f), "#F45");
// 	[field: SerializeField] public GffColor Orange { get; private set; } = new GffColor(new Color(0.93f, 0.59f, 0.18f), "#FA3");
// 	[field: SerializeField] public GffColor Yellow { get; private set; } = new GffColor(new Color(1f,    0.89f, 0.26f), "#FF5");
// 	[field: SerializeField] public GffColor Green  { get; private set; } = new GffColor(new Color(0.22f,  0.8f,  0.5f), "#4D8");
// 	[field: SerializeField] public GffColor Blue   { get; private set; } = new GffColor(new Color(0.19f, 0.63f, 0.69f), "#3AB");
// 	[field: SerializeField] public GffColor Purple { get; private set; } = new GffColor(new Color(0.89f, 0.27f, 0.69f), "#E4B");
// 	[field: SerializeField] public GffColor Violet { get; private set; } = new GffColor(new Color(0.49f, 0.38f, 0.76f), "#86D");
//
// 	internal GffColor FindColor(ColorType color) {
// 		switch(color) {
// 			default:
// 			case ColorType.Default: return DefaultConsoleColor;
// 			case ColorType.White : return White ;
// 			case ColorType.Grey  : return Grey  ;
// 			case ColorType.Black : return Black ;
// 			case ColorType.Red   : return Red   ;
// 			case ColorType.Orange: return Orange;
// 			case ColorType.Yellow: return Yellow;
// 			case ColorType.Green : return Green ;
// 			case ColorType.Blue  : return Blue  ;
// 			case ColorType.Purple: return Purple;
// 			case ColorType.Violet: return Violet;
// 		}
// 	}
// }
// }
//
// [PublicAPI]
// public interface IOnlyColor<T> {
//     public T White  { get; }
//     public T Grey   { get; }
//     public T Black  { get; }
//     public T Red    { get; }
//     public T Orange { get; }
//     public T Yellow { get; }
//     public T Green  { get; }
//     public T Blue   { get; }
//     public T Purple { get; }
//     public T Violet { get; }
// }
//
// namespace Graffiti {
// [PublicAPI]
// public partial class StyledString : IOnlyColor<StyledString> {
//
// 	public StyledString DefaultColor      => LastStyle.PrepareColor(ColorType.Default).Return(this);
// 	public StyledString White             => LastStyle.PrepareColor(ColorType.White  ).Return(this);
// 	public StyledString Grey              => LastStyle.PrepareColor(ColorType.Grey   ).Return(this);
// 	public StyledString Black             => LastStyle.PrepareColor(ColorType.Black  ).Return(this);
// 	public StyledString Red               => LastStyle.PrepareColor(ColorType.Red    ).Return(this);
// 	public StyledString Orange            => LastStyle.PrepareColor(ColorType.Orange ).Return(this);
// 	public StyledString Yellow            => LastStyle.PrepareColor(ColorType.Yellow ).Return(this);
// 	public StyledString Green             => LastStyle.PrepareColor(ColorType.Green  ).Return(this);
// 	public StyledString Blue              => LastStyle.PrepareColor(ColorType.Blue   ).Return(this);
// 	public StyledString Purple            => LastStyle.PrepareColor(ColorType.Purple ).Return(this);
// 	public StyledString Violet            => LastStyle.PrepareColor(ColorType.Violet ).Return(this);
//
// 	public StyledString None              => LastStyle.PrepareModifierCharacter(ModifierCharacterType.None             ).Return(this);
// 	public StyledString SmokingHot        => LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot       ).Return(this);
// 	public StyledString Strikethrough     => LastStyle.PrepareModifierCharacter(ModifierCharacterType.Strikethrough    ).Return(this);
// 	public StyledString WavyStrikethrough => LastStyle.PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough).Return(this);
// 	public StyledString Slash             => LastStyle.PrepareModifierCharacter(ModifierCharacterType.Slash            ).Return(this);
// 	public StyledString HighSlash         => LastStyle.PrepareModifierCharacter(ModifierCharacterType.HighSlash        ).Return(this);
// 	public StyledString Underline         => LastStyle.PrepareModifierCharacter(ModifierCharacterType.Underline        ).Return(this);
// 	public StyledString DoubleUnderline   => LastStyle.PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  ).Return(this);
// 	public StyledString Dotted            => LastStyle.PrepareModifierCharacter(ModifierCharacterType.Dotted           ).Return(this);
// 	public StyledString Wheel             => LastStyle.PrepareModifierCharacter(ModifierCharacterType.Wheel            ).Return(this);
// }
// }
//
// namespace Graffiti {
// [PublicAPI]
// public partial class StringStyle : IOnlyColor<StringStyle> {
//
// 	public StringStyle DefaultColor      => PrepareColor(ColorType.Default);
// 	public StringStyle White             => PrepareColor(ColorType.White );
// 	public StringStyle Grey              => PrepareColor(ColorType.Grey  );
// 	public StringStyle Black             => PrepareColor(ColorType.Black );
// 	public StringStyle Red               => PrepareColor(ColorType.Red   );
// 	public StringStyle Orange            => PrepareColor(ColorType.Orange);
// 	public StringStyle Yellow            => PrepareColor(ColorType.Yellow);
// 	public StringStyle Green             => PrepareColor(ColorType.Green );
// 	public StringStyle Blue              => PrepareColor(ColorType.Blue  );
// 	public StringStyle Purple            => PrepareColor(ColorType.Purple);
// 	public StringStyle Violet            => PrepareColor(ColorType.Violet);
//
// 	public StringStyle None              => PrepareModifierCharacter(ModifierCharacterType.None             );
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
// [PublicAPI]
// public static partial class Style {
//
// 	public static StringStyle DefaultColor      => StringStyle.Create().PrepareColor(ColorType.Default);
// 	public static StringStyle White             => StringStyle.Create().PrepareColor(ColorType.White );
// 	public static StringStyle Grey              => StringStyle.Create().PrepareColor(ColorType.Grey  );
// 	public static StringStyle Black             => StringStyle.Create().PrepareColor(ColorType.Black );
// 	public static StringStyle Red               => StringStyle.Create().PrepareColor(ColorType.Red   );
// 	public static StringStyle Orange            => StringStyle.Create().PrepareColor(ColorType.Orange);
// 	public static StringStyle Yellow            => StringStyle.Create().PrepareColor(ColorType.Yellow);
// 	public static StringStyle Green             => StringStyle.Create().PrepareColor(ColorType.Green );
// 	public static StringStyle Blue              => StringStyle.Create().PrepareColor(ColorType.Blue  );
// 	public static StringStyle Purple            => StringStyle.Create().PrepareColor(ColorType.Purple);
// 	public static StringStyle Violet            => StringStyle.Create().PrepareColor(ColorType.Violet);
//
// 	public static StringStyle None              => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.None             );
// 	public static StringStyle SmokingHot        => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot       );
// 	public static StringStyle Strikethrough     => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Strikethrough    );
// 	public static StringStyle WavyStrikethrough => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.WavyStrikethrough);
// 	public static StringStyle Slash             => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Slash            );
// 	public static StringStyle HighSlash         => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.HighSlash        );
// 	public static StringStyle Underline         => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Underline        );
// 	public static StringStyle DoubleUnderline   => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.DoubleUnderline  );
// 	public static StringStyle Dotted            => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Dotted           );
// 	public static StringStyle Wheel             => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.Wheel            );
// }
// }
