
using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

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

	public class CodeBlock : CodeBlockBase<CodeBlock>, IDisposable {
		public CodeBlock(CodeBuilderData data, string beforeOpen = "", string afterOpen = "", string afterClose = "", bool indentContent = true)
				: base(data, beforeOpen, afterOpen, afterClose, indentContent) { }

		public void Dispose() {
			throw new NotImplementedException();
		}
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
