using System;

namespace Graffiti.CodeGeneration {
/// <summary>
/// <inheritdoc cref="CodeBuilderBase{T}"/>
/// </summary>
public class CodeBlockBase<T> : CodeBuilderBase<T>, IDisposable
		where T : CodeBlockBase<T> {

	private readonly string _afterBlock;
	private readonly bool   _doIndentContent;

	protected CodeBlockBase(CodeBuilderInfo root,
	                        CodeBlockInfo   newBlockInfo,
	                        string          beforeBlock     = "",
	                        string          insideBlock     = "",
	                        string          afterBlock      = "",
	                        bool            doIndentContent = true)
			: base(root) {
		_afterBlock      = afterBlock;
		_doIndentContent = doIndentContent;
		InitCodeBlockData(newBlockInfo);
		Write($"{beforeBlock} {{\n{insideBlock}");
		OpenIndent();
	}

	public void Dispose() {
		DisposeCodeBlockData();
		CloseIndent();
		Write($"}}\n{_afterBlock}");
	}


	private void InitCodeBlockData(CodeBlockInfo info) {
		Root.CollectedBlockData.CopyTo(info);
		Root.ClearCollectedBlockData();
		Root.PushNewCodeBlockToStack(info);
	}

	private void DisposeCodeBlockData() => Root.PopCurrentCodeBlockFromStack();

	private void OpenIndent() {
		EnableIndent();
		if (_doIndentContent)
			IncreaseIndent();
	}

	private void CloseIndent() {
		if (_doIndentContent)
			DecreaseIndent();
		EnableIndent();
	}

}




}
