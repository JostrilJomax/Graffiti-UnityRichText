using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEditor.UI;

namespace Graffiti.CodeGeneration {
public class CodeBuilderGlue {

    public readonly  StringBuilder            Sb;
    private readonly Stack<CodeBlockInfo>     CodeBlockDataStack  = new Stack<CodeBlockInfo>();
    private          MemberInfo               CollectedMemberData = new MemberInfo();
    internal         CodeBuilderConfiguration BuilderConfig = new CodeBuilderConfiguration();
    public           int                      IndentLevel;
    public           bool                     WillIndentNewLine;
    public           bool                     IsExplicitStartOfNewLine = true;

    public CodeBuilderGlue(StringBuilder sb)
    {
        Sb = sb;
        CodeBlockDataStack.Push(CodeBlockInfo.CreateRootBlockInfo());
    }

    internal CodeBlockInfo CurrentCodeBlock => CodeBlockDataStack.Peek();

    internal void InitNewBlock()
    {
        var info = new CodeBlockInfo();
        CollectedMemberData.CopyMemberInfoTo(info);
        CollectedMemberData = new MemberInfo();
        CodeBlockDataStack.Push(info);
    }

    internal void ExitCurrentBlock()
        => CodeBlockDataStack.Pop();

}
}
