using System;
using Graffiti.CodeGeneration.Internal.Helpers;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.CodeGeneration {
[PublicAPI]
public abstract class CodeBlockBase<T> : CodeBuilderBase<T> where T : CodeBlockBase<T> {

    protected CodeBlockBase(CodeBuilderGlue glue) : base(glue) => glue.InitNewBlock();

    /// <summary> Body creation example: <para/> .Body( () => { '...body...' } ); </summary>
    public T Body(Action<T> body)
        => Body(() => body(this as T));

    /// <summary> Body creation example: <para/> .Body( Class => { '...body...' } ); </summary>
    public T Body([NotNull] Action body)
    {
        OpenBlock();
        body.Invoke();
        CloseBlock();
        return this as T;

        void OpenBlock()
        {
            Glue.OnOpenBlock();
            Write(" {").Br();
            IncreaseIndent();
        }

        void CloseBlock()
        {
            Glue.ExitCurrentBlock();
            DecreaseIndent();
            Write("}").Br();
        }
    }

    protected T WriteBlockName(string name)
    {
        string default_ = "_NameIsNotProvided_";
        Glue.SaveBlockName(name = name.SelfOrDefault(default_, false));
        return Write(name);
    }

}
}
