using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEditor.UI;
using UnityEngine;

namespace Graffiti.CodeGeneration {
public class CodeBuilderGlue {

    public readonly  StringBuilder            Sb;
    private readonly Stack<CodeBlockInfo>     CodeBlockDataStack  = new Stack<CodeBlockInfo>();
    private          CodeBlockInfo            PaddingCodeBlock;
    private          MemberInfo               CollectedMemberData = new MemberInfo();
    internal         CodeBuilderConfiguration BuilderConfig       = new CodeBuilderConfiguration();
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
        PaddingCodeBlock = new CodeBlockInfo();
        CollectedMemberData.CopyMemberInfoTo(PaddingCodeBlock);
        CollectedMemberData = new MemberInfo();
    }

    internal void OnOpenBlock()
    {
        CodeBlockDataStack.Push(PaddingCodeBlock);
        PaddingCodeBlock = null;
    }

    internal void ExitCurrentBlock()
        => CodeBlockDataStack.Pop();

    public void SaveBlockName(string name)
        => PaddingCodeBlock.Name = name;

}
}
