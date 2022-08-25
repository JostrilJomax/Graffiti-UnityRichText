using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.CodeGeneration {
/// <summary> This class contains all non-generic shared methods. See <see cref="CodeBuilderBase{T}"/> for more info. </summary>
public abstract class CodeBuilderBase {

    /// <summary> All shared info/objects are stored here </summary>
    protected internal CodeBuilderGlue Glue { get; }
    protected internal CodeBuilderConfiguration Config => Glue.BuilderConfig;

    protected CodeBuilderBase(CodeBuilderGlue glue)
    {
        Glue = glue;
    }

    protected void IncreaseIndent() => Glue.IndentLevel++;
    protected void DecreaseIndent() => Glue.IndentLevel = Math.Max(--Glue.IndentLevel, 0);

    public override string ToString() => Glue.Sb.ToString();

}

/// <summary>
///     This class contains all generic shared methods.
///     <para/>
///     This class exists to allow contained methods to directly return provided type T, which allows anyone who implements
///     this class to be able to use the FluentAPI without the need of constant cast to their type.
/// </summary>
public abstract class CodeBuilderBase<T> : CodeBuilderBase where T : CodeBuilderBase<T> {

    protected CodeBuilderBase(CodeBuilderGlue glue) : base(glue) { }

    public T Private  => (Glue.CurrentCodeBlock.IsPrivate  = true).Return(Write("private ") );
    public T Public   => (Glue.CurrentCodeBlock.IsPublic   = true).Return(Write("public ")  );
    public T Internal => (Glue.CurrentCodeBlock.IsInternal = true).Return(Write("internal "));
    public T Static   => (Glue.CurrentCodeBlock.IsStatic   = true).Return(Write("static ")  );
    public T Abstract => (Glue.CurrentCodeBlock.IsAbstract = true).Return(Write("abstract "));
    public T Partial  => (Glue.CurrentCodeBlock.IsPartial  = true).Return(Write("partial ") );

    /// <summary> Appends '\n', breaking current line. Activates indentation on the next line. </summary>
    /// <remarks> Name "br" stands for "break line" </remarks>
    public T Br()
    {
        Append('\n');
        Glue.IsExplicitStartOfNewLine = true;
        Glue.WillIndentNewLine = true;
        return this as T;
    }

    /// <summary>
    ///     Appends <see cref="value"/> on the current line, then appends '/n', breaking current line. Activates
    ///     indentation on the next line.
    /// </summary>
    public T Writeln(string value) => Write(value).Br();

    /// <summary>
    ///     Indents (if needed), then appends <see cref="value"/> on the current line. Disables indentation on the next
    ///     line (if current line was indented).
    /// </summary>
    public T Write(string value)
    {
        if (Glue.IsExplicitStartOfNewLine) {
            if (Config.DoCommentAll)
                Append("// ");
            Glue.IsExplicitStartOfNewLine = false;
        }

        Indent();
        Append(value);
        return this as T;
    }

    protected T Append(string value) => Glue.Sb.Append(value).Return(this as T);
    protected T Append(char value)   => Glue.Sb.Append(value).Return(this as T);

    /// <summary>
    ///     Appends indentation on the current line, according to the current indent level. If indent level = 0, will do
    ///     nothing.
    /// </summary>
    private T Indent(bool forceIndent = false)
    {
        if (!forceIndent && !Glue.WillIndentNewLine)
            return this as T;

        for (int i = 0; i < Glue.IndentLevel; i++)
            Append("    ");
        Glue.WillIndentNewLine = false;
        return this as T;
    }

}
}
