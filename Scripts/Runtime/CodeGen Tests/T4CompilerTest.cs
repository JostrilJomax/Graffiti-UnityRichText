using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Graffiti.Internal;
using Graffiti.CodeBuilding;
using JetBrains.Annotations;

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

namespace Graffiti.CodeBuilding {

public class CodeBuilder : CodeBuilderBase<CodeBuilder> {

	public CodeBuilder(StringBuilder sb) : base(new CodeBuilderData(sb)) { }

	public CodeBuilder Using(string @namespace)       => Writeln($"using {@namespace};");
	public CodeBuilder Comment(string comment)        => Writeln($"// {@comment}");
	public CodeBuilder AppendComment(string comment)  => Write($"// {@comment}");

	public CodeBuilder Header(string className, string pathToClass) {
		Writeln("//--------------------------------------------------------------------------------------");
		Writeln("// This file is generated. Modifications to this file won't be saved.");
		Writeln("// Last generated: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
		Writeln("// If you want to make any permanent changes to this file, go to the class " + className);
		Writeln("// At: " + pathToClass);
		Writeln("//--------------------------------------------------------------------------------------");
		return this;
	}

	//public CodeBuilder this[[NotNull] params string[] attributes] => Write($"[{string.Join(", ", attributes)}]");

	public CodeBuilder PublicAPI            => Write("[PublicAPI] ");
	public CodeBuilder SerializeField       => Write("[SerializeField] ");
	public CodeBuilder field_SerializeField => Write("[field: SerializeField] ");

	public PropertyBuilder Property(string type, string name)
		=> new PropertyBuilder(Data, type, name);

	public void Property(string type, string name, string body)
		=> Writeln($"{type} {name} {body}");

	public MethodBlock Method(string returnType, string name, [CanBeNull] params (string type, string name)[] Params)
		=> new MethodBlock(Data, returnType, name, Params);

	public CodeBlock Namespace(string name) => new CodeBlock(Data, $"namespace {name}", afterClose: "\n", indentContent: false);
	public CodeBlock Interface(string name) => new CodeBlock(Data, $"interface {name}");
	public CodeBlock Class(string     name) => new CodeBlock(Data, $"class {name}");

	public EnumBlock   Enum(string   name)  => new EnumBlock(Data, $"enum {name}");
	public SwitchBlock Switch(string param) => new SwitchBlock(Data, $"switch({param})");


	public class MethodBlock : CodeBlockBase<MethodBlock> {
		public readonly (string type, string name)[] Params;
		public MethodBlock(CodeBuilderData data, string returnType, string name, [CanBeNull] (string type, string name)[] Params)
				: base(data, beforeOpen: $"{returnType} {name}({(Params == null ? "" : string.Join(", ", Params.ToList().ConvertAll(x => $"{x.type} {x.name}")))})") {
			this.Params = Params;
		}
	}

	public class PropertyBuilder : CodeBuilderBase<PropertyBuilder> {
		public PropertyBuilder(CodeBuilderData data, string returnType, string name) : base(data)
			=> Write($"{returnType} {name}");

		public PropertyBuilder GetPrivateSet([CanBeNull] string setTo = null) => Writeln($" {{ get; private set; }}{(setTo == null ? "" : $" = {setTo};")}");

		public PropertyBuilder GetReturnThis(string body) => Writeln($" {{ get {{ {body}; return this; }} }}");

		public PropertyBuilder LambdaExpression([NotNull] string body) => Writeln($" => {body};");
	}

	public class EnumBlock : CodeBlockBase<EnumBlock> {
		public EnumBlock(CodeBuilderData data, string beforeOpen) : base(data, beforeOpen) { }
		public EnumBlock DefaultMember()       => Member("Default");
		public EnumBlock Member(string   name) => Writeln($"{name}, ");
	}

	public class SwitchBlock : CodeBlockBase<SwitchBlock> {
		public SwitchBlock(CodeBuilderData data, string beforeOpen) : base(data, beforeOpen) { }
		public SwitchBlock DefaultCase()        => Write($"default:");
		public SwitchBlock Case(string   name)  => Write($"case {name}: ");
		public SwitchBlock Return(string value) => Writeln($"return {value};");
	}

	public class CodeBlock : CodeBlockBase<CodeBlock> {
		public CodeBlock(CodeBuilderData data, string beforeOpen = "", string afterOpen = "", string afterClose = "", bool indentContent = true)
				: base(data, beforeOpen, afterOpen, afterClose, indentContent) { }
	}
}

/// <summary>
/// <inheritdoc cref="CodeBuilderBase{T}"/>
/// </summary>
public class CodeBlockBase<T> : CodeBuilderBase<T>, IDisposable
		where T : CodeBlockBase<T>
{
	private readonly string _afterClose;
	private readonly bool   _indentContent;

	protected CodeBlockBase(CodeBuilderData data, string beforeOpen = "", string afterOpen = "", string afterClose = "", bool indentContent = true)
			: base(data) {
		Data           = data;
		_afterClose    = afterClose;
		_indentContent = indentContent;
		Write($"{beforeOpen} {{\n{afterOpen}");
		EnableIndent();
		if (_indentContent)
			IncreaseIndent();
	}

	public void Dispose() {
		if (_indentContent)
			DecreaseIndent();
		EnableIndent();
		Write($"}}\n{_afterClose}");
	}
}


/// <summary>
/// This class is made to allow contained methods to directly return provided type T,
/// which allows anyone who implements this class to be able to use the FluentAPI.
/// </summary>
public abstract class CodeBuilderBase<T>
		where T : CodeBuilderBase<T> {

	public CodeBuilderData Data;

	public T Internal => Write("internal ");
	public T Public   => Write("public ");
	public T Private  => Write("private ");
	public T Static   => Write("static ");

	public T Abstract => Write("abstract ");
	public T Partial  => Write("partial ");

	/// <summary> Appends '\n', breaking current line. Activates indentation on the next line. </summary>
	public T br() {
		Data.Sb.Append('\n');
		EnableIndent();
		return this as T;
	}

	/// <summary> Appends <see cref="value"/> on the current line, then appends '/n', breaking current line. Activates indentation on the next line. </summary>
	public T Writeln(string value) => Write(value).br();
	/// <summary> Indents (if needed), then appends <see cref="value"/> on the current line. Disables indentation (if indented). </summary>
	public T Write(string value) {
		if (Data.IsIndentNeeded) {
			Indent();
			DisableIndent();
		}
		Data.Sb.Append(value);
		return this as T;
	}

	/// <summary> Appends indentation on the current line, according to the current indent level. If indent level = 0, will do nothing.  </summary>
	public T Indent() {
		for (int j = 0; j < Data.IndentLevel; j++)
			Data.Sb.Append("	");
		return this as T;
	}

	protected internal void IncreaseIndent() => Data.IndentLevel++;
	protected          void DecreaseIndent() => Data.IndentLevel = Math.Max(--Data.IndentLevel, 0);

	protected void EnableIndent()  => Data.IsIndentNeeded = true;
	protected void DisableIndent() => Data.IsIndentNeeded = false;

	protected CodeBuilderBase(CodeBuilderData cbd) => Data = cbd;
	public override string ToString() => Data.Sb.ToString();
}

public class CodeBuilderData {
	public readonly StringBuilder Sb;

	public bool IsIndentNeeded;
	public int  IndentLevel;

	public CodeBuilderData(StringBuilder sb) {
		Sb = sb;
	}
}
}
