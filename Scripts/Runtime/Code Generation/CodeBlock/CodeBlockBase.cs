using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.CodeGeneration {
public class CodeBlockBase<T> : CodeBuilderBase<T> where T : CodeBlockBase<T> {

    private CodeBlockInfo _blockInfo;

    protected bool _doIndentContent;

    protected CodeBlockBase(CodeBuilderInfo root) : base(root)
    {
        _blockInfo = new CodeBlockInfo();
    }

    public T Body([NotNull] Action<T> body)
    {
        T this_ = this as T;
        StartBlock();
        body.Invoke(this_);
        CloseBlock();
        return this_;

        void StartBlock()
        {
            InitCodeBlockData(_blockInfo);
            Write(" {\n");
            OpenIndent();
        }

        void CloseBlock()
        {
            ExitCodeBlock();
            CloseIndent();
            Write("}\n");
        }

        void InitCodeBlockData(CodeBlockInfo info)
        {
            Root.CollectedBlockData.CopyTo(info);
            Root.ClearCollectedBlockData();
            Root.PushNewCodeBlockToStack(info);
        }

        void ExitCodeBlock() => Root.PopCurrentCodeBlockFromStack();

        void OpenIndent()
        {
            EnableIndent();
            if (_doIndentContent)
                IncreaseIndent();
        }

        void CloseIndent()
        {
            if (_doIndentContent)
                DecreaseIndent();
            EnableIndent();
        }
    }

}
}
