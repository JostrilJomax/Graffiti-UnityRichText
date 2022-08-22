using System;
using System.Text;
using JetBrains.Annotations;

namespace Graffiti.CodeGeneration {
public class CodeBuilder : CodeBuilderBase<CodeBuilder> {

	private CodeBuilder(CodeBuilderInfo root) : base(root) { }

	public static CodeBuilder CreateDefaultBuilder() => new CodeBuilder(new CodeBuilderInfo(new StringBuilder()));


	// ---------------------------------------------------------
	// Miscellaneous
	// ---------------------------------------------------------

	public CodeBuilder Header(string className) {
		Writeln("//--------------------------------------------------------------------------------------");
		Writeln("// This file is generated. Modifications to this file won't be saved.");
		Writeln("// Last generated: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
		Writeln("// If you want to make any permanent changes to this file, go to the class " + className);
		Writeln("// At: ???");
		Writeln("//--------------------------------------------------------------------------------------");
		return this;
	}
	public CodeBuilder Using(string @namespace) => Writeln($"using {@namespace};");


	// ---------------------------------------------------------
	// Attributes & Indexers
	// ---------------------------------------------------------

	public CodeBuilder PublicAPI            => Write("[PublicAPI] ");
	public CodeBuilder SerializeField       => Write("[SerializeField] ");
	public CodeBuilder field_SerializeField => Write("[field: SerializeField] ");

	// These indexers could potentially make the attributes more explicit in code generation, but currently they don't.
	//public CodeBuilder this[[NotNull] params string[] attributes] => Write($"[{string.Join(", ", attributes)}]");
	//public CodeBuilder this[[NotNull] params CodeBuilder[] self] => self[^0];


	// ---------------------------------------------------------
	// CodeBuilders
	// ---------------------------------------------------------

	public void            Property (string returns, string name, string body) => Writeln($"{returns} {name} {body}");
	public PropertyBuilder Property(string  returns, string name) => new PropertyBuilder(Root, returns, name);


	// ---------------------------------------------------------
	// Code Blocks
	// ---------------------------------------------------------

	public CodeBlock Namespace     (string name) => new CodeBlock(Root, CodeBlockInfo.Create(name), $"namespace {name}", afterBlock: "\n", doIndentContent: false);
	public CodeBlock Interface     (string name) => new CodeBlock(Root, CodeBlockInfo.Create(name), $"interface {name}");

	public CodeBlock Class(string name, string inherit = "")
		=> new CodeBlock(Root, CodeBlockInfo.Create(name),
			$"class {name}{(string.IsNullOrEmpty(inherit) ? "" : $" : {inherit}")}");

	public void Method(string returns, string name, (string type, string name) param0,Action<MethodBlock> body)                                                                        => Method(returns, name, body, param0);
	public void Method(string returns, string name, (string type, string name) param0, (string type, string name) param1,Action<MethodBlock> body)                                     => Method(returns, name, body, param0, param1);
	public void Method(string returns, string name, (string type, string name) param0, (string type, string name) param1, (string type, string name) param2, Action<MethodBlock> body) => Method(returns, name, body, param0, param1, param2);
	public void Method(string returns, string name, Action<MethodBlock> body, params (string type, string name)[] params_) {
		using var block = new MethodBlock(Root, returns, name, params_);
		body.Invoke(block);
	}

	public void Enum(string name, Action<EnumBlock> body) {
		using var block = new EnumBlock(Root, name);
		body.Invoke(block);
	}

	public void Switch(string name, Action<SwitchBlock> body) {
		using var block = new SwitchBlock(Root, name);
		body.Invoke(block);
	}
}
}
