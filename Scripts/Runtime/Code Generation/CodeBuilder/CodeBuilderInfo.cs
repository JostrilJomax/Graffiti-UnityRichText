using System.Collections.Generic;
using System.Text;
using Graffiti.CodeGeneration;

namespace Graffiti.CodeGeneration {
public class CodeBuilderInfo {

	public readonly StringBuilder        Sb;
	public          MemberModifiers      CollectedBlockData = new MemberModifiers();
	private         Stack<CodeBlockInfo> CodeBlockDataStack = new Stack<CodeBlockInfo>();

	public bool IsIndentNeeded;
	public int  IndentLevel;

	public CodeBuilderInfo(StringBuilder sb) => Sb = sb;

	internal void          PushNewCodeBlockToStack(CodeBlockInfo newCodeBlockInfo) => CodeBlockDataStack.Push(newCodeBlockInfo);
	internal void          PopCurrentCodeBlockFromStack()                          => CodeBlockDataStack.Pop();
	internal CodeBlockInfo CurrentCodeBlock                                        => CodeBlockDataStack.Peek();
	internal void          ClearCollectedBlockData()                               => CollectedBlockData = new MemberModifiers();
}
}
