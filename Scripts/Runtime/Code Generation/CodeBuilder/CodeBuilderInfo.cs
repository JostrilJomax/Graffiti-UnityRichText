using System.Collections.Generic;
using System.Text;

namespace Graffiti.CodeGeneration {
public class CodeBuilderInfo {

    private readonly Stack<CodeBlockInfo> CodeBlockDataStack = new Stack<CodeBlockInfo>();
    public readonly  StringBuilder        Sb;

    public MemberModifiers          CollectedBlockData = new MemberModifiers();
    public CodeBuilderConfiguration Config;
    public int                      IndentLevel;

    public bool IsIndentNeeded;

    public CodeBuilderInfo(StringBuilder sb)
    {
        Sb = sb;
    }

    internal CodeBlockInfo CurrentCodeBlock => CodeBlockDataStack.Peek();

    internal void PushNewCodeBlockToStack(CodeBlockInfo newCodeBlockInfo) => CodeBlockDataStack.Push(newCodeBlockInfo);
    internal void PopCurrentCodeBlockFromStack()                          => CodeBlockDataStack.Pop();
    internal void ClearCollectedBlockData()                               => CollectedBlockData = new MemberModifiers();

}
}
