using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Graffiti.Internal;
using Graffiti.CodeBuilding;
using JetBrains.Annotations;

namespace Graffiti {
public static class T4CompilerTest {

	static readonly string _Graffiti              = "Graffiti";
	static readonly string _Graffiti_Internal     = "Graffiti.Internal";
	static readonly string _ColorType             = "ColorType";
	static readonly string _ModifierCharacterType = nameof(global::Graffiti.Internal.ModifierCharacterType);
	static readonly string _ColorPalette          = nameof(global::Graffiti.ColorPalette);
	static readonly string _GffColor              = nameof(global::Graffiti.GffColor);
	static readonly string _StyledString          = nameof(global::Graffiti.StyledString);
	static readonly string _StringStyle           = nameof(global::Graffiti.StringStyle);
	static readonly string _IOnlyColor            = "IOnlyColor";

	static readonly string _PrepareColor             = nameof(Style.DefaultColor.PrepareColor);
	static readonly string _PrepareModifierCharacter = nameof(Style.DefaultColor.PrepareModifierCharacter);

	[InitializeOnLoadMethod]
	public static void Build() {
		string classPath         = GraffitiAssetDatabase.FindFullPathToFile(nameof(T4CompilerTest), true)[0];
		string generatedFilePath = classPath.Replace($"{nameof(T4CompilerTest)}.cs", "Hi.txt");

		var cb = new CodeBuilder(new StringBuilder());

		cb.AppendHeader(nameof(T4CompilerTest), classPath).br();

		cb.Using("JetBrains.Annotations");
		cb.Using("UnityEngine");
		cb.Using(_Graffiti_Internal);
		cb.Using(_Graffiti).br();

		using (cb.Namespace(_Graffiti_Internal)) {
			using (cb.Internal_Enum(_ColorType)) {
				cb.NewLine($"Default,");
				foreach(var field in _colorFields)
					cb.NewLine($"{field.name},");
			}
		}

		using (cb.Namespace(_Graffiti)) {
			using (cb.Public_Partial_Class(_ColorPalette)) {

				// [field: SerializeField] public GffColor White  { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
				foreach(var field in _colorFields)
					cb.NewLine($"[field: SerializeField] public {_GffColor} {field.trimmedName} {{ get; private set; }} = new {_GffColor}({field.UnityColor}, \"{field.ShortHexColor}\");");
				cb.br();

				string paramName = "color";
				string methodName  = "FindColor";

				using (cb.CodeBlock($"internal {_GffColor} {methodName}({_ColorType} {paramName})")) {
					using (cb.Switch(paramName)) {
						cb.NewLine("default:");
						cb.NewLine($"case {_ColorType}.Default: return {nameof(ColorPalette.DefaultConsoleColor)};");
						_colorFields.ForEach(field =>
							cb.NewLine($"case {_ColorType}.{field.trimmedName}: return {field.trimmedName};"));
					}
				}
			}
		}
		
		using (cb.Namespace(_Graffiti)) {
			using (cb.Public_Partial_Class($"{_StringStyle} : {_StringStyle}.{_IOnlyColor}")) {

				using (cb.Interface(_IOnlyColor))
					foreach(var field in _colorFields)
						cb.NewLine($"public {_StringStyle} {field.trimmedName} {{ get; }}");

				string getterBody = $"[PublicAPI] public {_StringStyle}";

				// [PublicAPI] public StringStyle White => PrepareColor(ColorType.White);
				foreach(var field in _colorFields)
					cb.NewLine($"{getterBody} {field.trimmedName} => {_PrepareColor}({_ColorType}.{field.trimmedName});");
				cb.br();

				// [PublicAPI] public StringStyle SmokingHot => PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
				foreach (var field in _modifierCharacterFields)
					cb.NewLine($"{getterBody} {field.trimmedName} => {_PrepareModifierCharacter}({_ModifierCharacterType}.{field.trimmedName});");
				cb.br();
			}
		}

		using (cb.Namespace(_Graffiti)) {
			using (cb.Public_Partial_Class($"{_StyledString} : {_StyledString}.{_IOnlyColor}")) {

				using (cb.Interface(_IOnlyColor))
					foreach(var field in _colorFields)
						cb.NewLine($"public {_StyledString} {field.trimmedName} {{ get; }}");

				//[PublicAPI] public StyledString White  { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
				string getterBody = $"[PublicAPI] public {_StyledString}";
				foreach(var field in _colorFields)
					cb.NewLine($"{getterBody} {field.trimmedName} {{ get {{ LastStyle.{_PrepareColor}({_ColorType}.{field.trimmedName}); return this; }} }}");
				cb.br();
				_modifierCharacterFields.ForEach(field => cb.NewLine($"{getterBody} {field.trimmedName} {{ get {{ LastStyle.{_PrepareModifierCharacter}({_ModifierCharacterType}.{field.trimmedName}); return this; }} }}"));
				cb.br();
			}
		}

		Task task = Write(generatedFilePath, cb.ToString());
	}

	private static async Task Write(string path, string text) {
		Debug.Log("Started writing at: " + path);
		await File.WriteAllTextAsync(path, text);
	}




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

	private struct ColorField {
		public readonly string name, space, trimmedName, UnityColor, ShortHexColor;
		public ColorField(string fieldName, string field_space, string unityColor, string shortHexColor) {
			name = fieldName; space = field_space; trimmedName = fieldName + field_space;
			UnityColor    = unityColor;
			ShortHexColor = shortHexColor;
		}
	}


	private static readonly List<UnityFontStyleField> _unityFontStyleFields = new List<UnityFontStyleField> {
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.None), "      "),
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.Bold), "      "),
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.Italic), "    "),
			new UnityFontStyleField(nameof(UnityBuildInFontStyleType.BoldItalic), ""),
	};

	private struct UnityFontStyleField {
		public readonly string name, space, trimmedName;
		public UnityFontStyleField(string fieldName, string field_space) {
			name = fieldName; space = field_space; trimmedName = fieldName + field_space;
		}
	}

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

	private struct ModifierCharacterField {
		public readonly string name, space, trimmedName;
		public ModifierCharacterField(string fieldName, string field_space) {
			name = fieldName; space = field_space; trimmedName = fieldName + field_space;
		}
	}
}
}

namespace Graffiti.CodeBuilding {
public class CodeBuilder {
	public StringBuilder SB;
	public int           IndentLevel;

	public CodeBuilder(StringBuilder sb) => this.SB = sb;
	public override string ToString() => SB.ToString();

	public CodeBuilder IncreaseIndent() { IndentLevel++; return this; }
	public CodeBuilder DecreaseIndent() { IndentLevel = Math.Max(--IndentLevel, 0); return this; }

	public CodeBuilder br()                      { SB.Append('\n');                       return this; }
	public CodeBuilder NewLine(string value)          { Indent(SB).Append(value).Append('\n'); return this; }
	public CodeBuilder Append(char value)             { Indent(SB).Append(value);              return this; }
	public CodeBuilder Append(string value)           { Indent(SB).Append(value);              return this; }

	public CodeBuilder Using(string @namespace)       { NewLine($"using {@namespace};");       return this; }

	public CodeBuilder Comment(string comment)        { NewLine($"// {@comment}");             return this; }
	public CodeBuilder AppendComment(string comment)  { Append($"// {@comment}");              return this; }


	public CodeBuilder AppendHeader(string className, string pathToClass) {
		NewLine("//--------------------------------------------------------------------------------------");
		NewLine("// This file is generated. Modifications to this file won't be saved.");
		NewLine("// Last generated: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
		NewLine("// If you want to make any permanent changes to this file, go to the class " + className);
		NewLine("// At: " + pathToClass);
		NewLine("//--------------------------------------------------------------------------------------");
		return this;
	}

	private StringBuilder Indent(StringBuilder sb) { for (int j = 0; j < IndentLevel; j++) sb.Append("	"); return this.SB; }


	public CodeBlock CodeBlock(string                beforeOpen) => new CodeBlock(this, beforeOpen);
	public CodeBlock Switch(string                   param)      => new CodeBlock(this, $"switch({param})");
	public CodeBlock Namespace(string                name)       => new CodeBlock(this, $"namespace {name}", afterClose: "\n", indentContent: false);
	public CodeBlock Interface(string                name)       => new CodeBlock(this, $"public interface {name}", afterClose: "\n");
	public CodeBlock Class(string                    name)       => new CodeBlock(this, $"public class {name}", afterOpen: "\n");
	public CodeBlock Public_Partial_Class(string     name)       => new CodeBlock(this, $"public partial class {name}", afterOpen: "\n");
	public CodeBlock Public_Static_Partial_Class(string name)       => new CodeBlock(this, $"public static partial class {name}", afterOpen: "\n");
	public CodeBlock Internal_Enum(string            name)       => new CodeBlock(this, $"internal enum {name}");
}

public class CodeBlock : IDisposable {
	private readonly CodeBuilder _cb;
	private readonly string      _afterClose;
	private readonly bool        _indentContent;

	public CodeBlock(CodeBuilder cb, string beforeOpen, string afterOpen = "", string afterClose = "", bool indentContent = true) {
		_cb         = cb;
		_afterClose = afterClose;
		_indentContent     = indentContent;
		_cb.Append($"{beforeOpen} {{\n{afterOpen}");
		if (_indentContent)
			_cb.IncreaseIndent();
	}

	public void Dispose() {
		if (_indentContent)
			_cb.DecreaseIndent();
		_cb.Append($"}}\n{_afterClose}");
	}
}
}
