using System.Collections.Generic;
using Graffiti.Internal;

namespace Graffiti.CodeGeneration {
public static class StylizationOptionsData {

    public static readonly List<ColorField> ColorFields = new List<ColorField> {
        new ColorField("White", " ", "new Color(0.93f, 0.93f, 0.93f)" /*"#eeeeee"*/, "#eee"),
        new ColorField("Grey", "  ", "new Color(0.67f, 0.67f, 0.67f)" /*"#aaaaaa"*/, "#aaa"),
        new ColorField("Black", " ", "new Color(0.07f, 0.07f, 0.07f)" /*"#111111"*/, "#111"),
        new ColorField("Red", "   ", "new Color(0.93f, 0.23f, 0.25f)" /*"#EC3A41"*/, "#F45"),
        new ColorField("Orange", "", "new Color(0.93f, 0.59f, 0.18f)" /*"#ED962F"*/, "#FA3"),
        new ColorField("Yellow", "", "new Color(1f,    0.89f, 0.26f)" /*"#FFE443"*/, "#FF5"),
        new ColorField("Green", " ", "new Color(0.22f,  0.8f,  0.5f)" /*"#39CD7F"*/, "#4D8"),
        new ColorField("Blue", "  ", "new Color(0.19f, 0.63f, 0.69f)" /*"#30a0b0"*/, "#3AB"),
        new ColorField("Purple", "", "new Color(0.89f, 0.27f, 0.69f)" /*"#E245B0"*/, "#E4B"),
        new ColorField("Violet", "", "new Color(0.49f, 0.38f, 0.76f)" /*"#7d60c3"*/, "#86D"),
    };

    public static readonly List<UnityFontStyleField> _unityFontStyleFields = new List<UnityFontStyleField> {
        new UnityFontStyleField(nameof(UnityBuildInFontStyleType.None), "      "),
        new UnityFontStyleField(nameof(UnityBuildInFontStyleType.Bold), "      "),
        new UnityFontStyleField(nameof(UnityBuildInFontStyleType.Italic), "    "),
        new UnityFontStyleField(nameof(UnityBuildInFontStyleType.BoldItalic), ""),
    };

    public static readonly List<ModifierCharacterField> ModifierCharacterFields = new List<ModifierCharacterField> {
        new ModifierCharacterField(nameof(ModifierCharacterType.None), "             "),
        new ModifierCharacterField(nameof(ModifierCharacterType.SmokingHot), "       "),
        new ModifierCharacterField(nameof(ModifierCharacterType.Strikethrough), "    "),
        new ModifierCharacterField(nameof(ModifierCharacterType.WavyStrikethrough), ""),
        new ModifierCharacterField(nameof(ModifierCharacterType.Slash), "            "),
        new ModifierCharacterField(nameof(ModifierCharacterType.HighSlash), "        "),
        new ModifierCharacterField(nameof(ModifierCharacterType.Underline), "        "),
        new ModifierCharacterField(nameof(ModifierCharacterType.DoubleUnderline), "  "),
        new ModifierCharacterField(nameof(ModifierCharacterType.Dotted), "           "),
        new ModifierCharacterField(nameof(ModifierCharacterType.Wheel), "            "),
    };


    public struct ColorField {

        public readonly string name, space, trimmedName, UnityColor, ShortHexColor;

        public ColorField(string fieldName, string field_space, string unityColor, string shortHexColor)
        {
            name = fieldName;
            space = field_space;
            trimmedName = fieldName + field_space;
            UnityColor = unityColor;
            ShortHexColor = shortHexColor;
        }

    }

    public struct UnityFontStyleField {

        public readonly string name, space, trimmedName;

        public UnityFontStyleField(string fieldName, string field_space)
        {
            name = fieldName;
            space = field_space;
            trimmedName = fieldName + field_space;
        }

    }

    public struct ModifierCharacterField {

        public readonly string name, space, trimmedName;

        public ModifierCharacterField(string fieldName, string field_space)
        {
            name = fieldName;
            space = field_space;
            trimmedName = fieldName + field_space;
        }

    }

}
}
