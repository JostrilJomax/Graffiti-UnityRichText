using System.Linq;
using JetBrains.Annotations;

namespace Graffiti.CodeGeneration {
public class PropertyBuilder : CodeBuilderBase<PropertyBuilder> {
	public PropertyBuilder(CodeBuilderInfo root, string returnType, string name)
			: base(root) => Write($"{returnType} {name}");

	public void GetPrivateSet([CanBeNull] string equateTo = null) => Writeln($" {{ get; private set; }}{(equateTo == null ? "" : $" = {equateTo};")}");
	public void GetReturnThis(string body)                     => Writeln($" {{ get {{ {body}; return this; }} }}");
	public void LambdaExpression([NotNull] string body)        => Writeln($" => {body};");
}


public class MethodBlock : CodeBlockBase<MethodBlock> {
	public readonly (string type, string name)[] Params;

	public MethodBlock(CodeBuilderInfo root, string returns, string name, [CanBeNull] (string type, string name)[] params_)
			: base(root, CodeBlockInfo.Create(name), $"{returns} {name}({(params_ == null ? "" : string.Join(", ", params_.ToList().ConvertAll(x => $"{x.type} {x.name}")))})") {
		Params = params_;
	}
}

public class EnumBlock : CodeBlockBase<EnumBlock> {
	public EnumBlock(CodeBuilderInfo root, string name, string insideBlock = "") : base(root, CodeBlockInfo.Create(name), $"enum {name}", insideBlock) { }
	public EnumBlock WriteDefaultMember()     => WriteMember("Default");
	public EnumBlock WriteMember(string name) => Writeln($"{name}, ");
}

public class SwitchBlock : CodeBlockBase<SwitchBlock> {
	public SwitchBlock(CodeBuilderInfo root, string name): base(root, CodeBlockInfo.Create(name), $"switch ({name})") { }
	public SwitchBlock WriteDefaultCase()      => Write("default:");
	public SwitchBlock WriteCase(string name)  => Write($"case {name}: ");
	public SwitchBlock Return(string    value) => Writeln($"return {value};");
	public SwitchBlock Break()                 => Writeln($"break;");
}

public class CodeBlock : CodeBlockBase<CodeBlock> {
	public CodeBlock(CodeBuilderInfo root,
	                 CodeBlockInfo   newBlockInfo,
	                 string          beforeBlock     = "",
	                 string          insideBlock     = "",
	                 string          afterBlock      = "",
	                 bool            doIndentContent = true)
			: base(root, newBlockInfo, beforeBlock, insideBlock, afterBlock, doIndentContent) { }
}
}
