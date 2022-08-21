using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Graffiti.Internal;
using Graffiti.CodeBuilding;

namespace Graffiti {
public static class T4CompilerTest {

	private static string _colorField_nameof_DefaultColor = "DefaultColor";

	private static readonly List<ColorField> _colorFields = new List<ColorField> {
			new ColorField("White"," ", "new Color(0.93f, 0.93f, 0.93f)" /*"#eeeeee"*/, "#eee"),
			new ColorField("Grey","  ", "new Color(0.67f, 0.67f, 0.67f)" /*"#aaaaaa"*/, "#aaa"),
			new ColorField("Black"," ", "new Color(0.07f, 0.07f, 0.07f)" /*"#111111"*/, "#111"),
			new ColorField("Red","   ", "new Color(0.93f, 0.23f, 0.25f)" /*"#EC3A41"*/, "#F45"),
			new ColorField("Orange","", "new Color(0.93f, 0.59f, 0.18f)" /*"#ED962F"*/, "#FA3"),
			new ColorField("Yellow","", "new Color(1f,    0.89f, 0.26f)" /*"#FFE443"*/, "#FF5"),
			new ColorField("Green"," ", "new Color(0.22f,  0.8f,  0.5f)" /*"#39CD7F"*/, "#4D8"),
			new ColorField("Blue","  ", "new Color(0.19f, 0.63f, 0.69f)" /*"#30a0b0"*/, "#3AB"),
			new ColorField("Purple","", "new Color(0.89f, 0.27f, 0.69f)" /*"#E245B0"*/, "#E4B"),
			new ColorField("Violet","", "new Color(0.49f, 0.38f, 0.76f)" /*"#7d60c3"*/, "#86D"),
	};

	private static readonly List<UnityFontStyleField> _unityFontStyleFields = new List<UnityFontStyleField> {
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.None), "      "),
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.Bold), "      "),
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.Italic), "    "),
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.BoldItalic), ""),
	};

	private static readonly List<ModifierCharacterField> _modifierCharacterFields = new List<ModifierCharacterField> {
			new ModifierCharacterField(nameof(ModifierCharacterType.None),"             "),
			new ModifierCharacterField(nameof(ModifierCharacterType.SmokingHot),"       "),
			new ModifierCharacterField(nameof(ModifierCharacterType.Strikethrough),"    "),
			new ModifierCharacterField(nameof(ModifierCharacterType.WavyStrikethrough),""),
			new ModifierCharacterField(nameof(ModifierCharacterType.Slash),"            "),
			new ModifierCharacterField(nameof(ModifierCharacterType.HighSlash),"        "),
			new ModifierCharacterField(nameof(ModifierCharacterType.Underline),"        "),
			new ModifierCharacterField(nameof(ModifierCharacterType.DoubleUnderline),"  "),
			new ModifierCharacterField(nameof(ModifierCharacterType.Dotted),"           "),
			new ModifierCharacterField(nameof(ModifierCharacterType.Wheel),"            "),
	};

	private static string _Graffiti                 = "Graffiti";
	private static string _Graffiti_Internal        = "Graffiti.Internal";
	private static string _ColorType                = "ColorType";
	private static string _ColorType_Default        = "ColorType.Default";
	private static string _nameof_ColorType_Default = "Default";
	private static string _ModifierCharacterType    = nameof(global::Graffiti.Internal.ModifierCharacterType);
	private static string _ColorPalette             = nameof(global::Graffiti.ColorPalette);
	private static string _GffColor                 = nameof(global::Graffiti.GffColor);
	private static string _StyledString             = nameof(global::Graffiti.StyledString);
	private static string _StringStyle              = nameof(global::Graffiti.StringStyle);
	private static string _Style                    = nameof(global::Graffiti.Style);
	private static string _IOnlyColor               = "IOnlyColor";

	[InitializeOnLoadMethod]
	public static void Build() {
		string classPath         = GraffitiAssetDatabase.FindFullPathToFile(nameof(T4CompilerTest), true)[0];
		string generatedFilePath = classPath.Replace($"{nameof(T4CompilerTest)}.cs", "Partial.GeneratedMembers.cs");

		if (File.Exists(generatedFilePath))
			return;


		var cb = new CodeBuilder(new StringBuilder());

		cb.Header(nameof(T4CompilerTest), classPath).br();

		cb.Using("JetBrains.Annotations");
		cb.Using("UnityEngine");
		cb.Using(_Graffiti_Internal).br();

		using (cb.Namespace(_Graffiti_Internal)) {
			using (var Enum = cb.Internal.Enum(_ColorType)) {
				Enum.DefaultMember();
				foreach(var field in _colorFields)
					Enum.Member(field.name);
			}
		}

		using (cb.Namespace(_Graffiti)) {
			cb.PublicAPI.br();
			using (cb.Public.Partial.Class(_ColorPalette).br()) {

				// [field: SerializeField] public GffColor White  { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
				foreach(var field in _colorFields)
					cb.field_SerializeField.Public.Property(type: _GffColor, name: field.trimmedName)
					  .GetPrivateSet(setTo: _new_GffColor(field.UnityColor, field.ShortHexColor));
				cb.br();

				using (var Method = cb.Internal.Method(returnType: _GffColor, name: "FindColor", Params: (_ColorType, "color"))) {
					using (var Switch = cb.Switch(Method.Params[0].name)) {
						Switch.DefaultCase().br();
						Switch.Case(_ColorType_Default).Return(nameof(ColorPalette.DefaultConsoleColor));
						foreach(var field in _colorFields)
							Switch.Case($"{_ColorType}.{field.trimmedName}").Return(field.trimmedName);
					}
				}
			}
		}

		using (cb.Namespace(_Graffiti)) {
			cb.PublicAPI.br();
			using (cb.Public.Partial.Class($"{_StringStyle} : {_StringStyle}.{_IOnlyColor}").br()) {

				WriteCommonInterfaces(_StringStyle);

				// public StringStyle White => PrepareColor(ColorType.White);
				WritePropertyFor_StringStyle(_colorField_nameof_DefaultColor, GetBodyOf_PrepareColor, _nameof_ColorType_Default);
				foreach(var field in _colorFields)
					WritePropertyFor_StringStyle(field.trimmedName, GetBodyOf_PrepareColor);
				cb.br();

				// public StringStyle SmokingHot => PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
				foreach (var field in _modifierCharacterFields)
					WritePropertyFor_StringStyle(field.trimmedName, GetBodyOf_PrepareModifierCharacter);
			}
		}

		using (cb.Namespace(_Graffiti)) {
			cb.PublicAPI.br();
			using (cb.Public.Partial.Class($"{_StyledString} : {_StyledString}.{_IOnlyColor}").br()) {

				WriteCommonInterfaces(_StyledString);

				// public StyledString White  { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
				WritePropertyFor_StyledString(_colorField_nameof_DefaultColor, GetBodyOf_PrepareColor, _nameof_ColorType_Default);
				foreach(var field in _colorFields)
					WritePropertyFor_StyledString(field.trimmedName, GetBodyOf_PrepareColor);
				cb.br();

				foreach(var field in _modifierCharacterFields)
					WritePropertyFor_StyledString(field.trimmedName, GetBodyOf_PrepareModifierCharacter);
			}
		}

		using (cb.Namespace(_Graffiti)) {
			cb.PublicAPI.br();
			using (cb.Public.Static.Partial.Class(_Style).br()) {

				// public static StringStyle White => StringStyle.Create().PrepareColor(ColorType.White);
				WritePropertyFor_Style(_colorField_nameof_DefaultColor, GetBodyOf_PrepareColor, _nameof_ColorType_Default);
				foreach(var field in _colorFields)
					WritePropertyFor_Style(field.trimmedName, GetBodyOf_PrepareColor);
				cb.br();

				// public static StringStyle SmokingHot => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
				foreach(var field in _modifierCharacterFields)
					WritePropertyFor_Style(field.trimmedName, GetBodyOf_PrepareModifierCharacter);
			}
		}

		Task task = Write(generatedFilePath, cb.ToString());

		void WritePropertyFor_StringStyle (string name, Func<string, string> getBody, string itemType = null) => cb.Public       .Property(_StringStyle,  name).LambdaExpression(getBody(itemType ?? name));
		void WritePropertyFor_StyledString(string name, Func<string, string> getBody, string itemType = null) => cb.Public       .Property(_StyledString, name).GetReturnThis   ("LastStyle." + getBody(itemType ?? name));
		void WritePropertyFor_Style       (string name, Func<string, string> getBody, string itemType = null) => cb.Public.Static.Property(_StringStyle,  name).LambdaExpression($"{_StringStyle_Create()}." + getBody(itemType ?? name));

		void WriteCommonInterfaces(string returnType) {
			cb.PublicAPI.br();
			using (cb.Public.Interface(_IOnlyColor))
				foreach (var field in _colorFields)
					cb.Public.Writeln($"{returnType} {field.trimmedName} {{ get; }}");
			cb.br();
		}

		string GetBodyOf_PrepareColor            (string itemType) => $"{nameof(StringStyle.PrepareColor)}({_ColorType}.{itemType})";
		string GetBodyOf_PrepareModifierCharacter(string itemType) => $"{nameof(StringStyle.PrepareModifierCharacter)}({_ModifierCharacterType}.{itemType})";

		string _new_GffColor(string unityColor, string shortHexColor) => $"new {_GffColor}({unityColor}, \"{shortHexColor}\")";
		string _StringStyle_Create() => $"{_StringStyle}.{nameof(StringStyle.Create)}()";
	}

	private static async Task Write(string path, string text) {
		Debug.Log("Started writing at: " + path);
		await File.WriteAllTextAsync(path, text);
	}


	private struct ColorField {
		public readonly string name, space, trimmedName, UnityColor, ShortHexColor;
		public ColorField(string fieldName, string field_space, string unityColor, string shortHexColor) {
			name = fieldName; space = field_space; trimmedName = fieldName + field_space;
			UnityColor    = unityColor;
			ShortHexColor = shortHexColor;
		}
	}

	private struct UnityFontStyleField {
		public readonly string name, space, trimmedName;
		public UnityFontStyleField(string fieldName, string field_space) {
			name = fieldName; space = field_space; trimmedName = fieldName + field_space;
		}
	}

	private struct ModifierCharacterField {
		public readonly string name, space, trimmedName;
		public ModifierCharacterField(string fieldName, string field_space) {
			name = fieldName; space = field_space; trimmedName = fieldName + field_space;
		}
	}
}
}
