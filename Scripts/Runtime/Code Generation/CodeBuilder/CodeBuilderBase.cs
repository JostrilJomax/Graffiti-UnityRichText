using System;

namespace Graffiti.CodeGeneration {
/// <summary> This class contains all non-generic shared methods. See <see cref="CodeBuilderBase{T}"/> for more info. </summary>
public class CodeBuilderBase {
    protected CodeBuilderBase(CodeBuilderInfo root)
    {
        Root = root;
    }

    protected internal CodeBuilderInfo Root { get; }

    protected       void   IncreaseIndent() => Root.IndentLevel++;
    protected       void   DecreaseIndent() => Root.IndentLevel = Math.Max(--Root.IndentLevel, 0);
    protected       void   EnableIndent()   => Root.IsIndentNeeded = true;
    protected       void   DisableIndent()  => Root.IsIndentNeeded = false;
    public override string ToString()       => Root.Sb.ToString();
}

/// <summary>
///     This class contains all generic shared methods.
///     <para/>
///     This class exists to allow contained methods to directly return provided type T, which allows anyone who implements
///     this class to be able to use the FluentAPI without the need of constant cast to their type.
/// </summary>
public class CodeBuilderBase<T> : CodeBuilderBase where T : CodeBuilderBase<T> {
    protected CodeBuilderBase(CodeBuilderInfo root) : base(root) { }

    public T Private {
        get {
            Root.CurrentCodeBlock.IsPrivate = true;
            return Write("private ");
        }
    }

    public T Public {
        get {
            Root.CurrentCodeBlock.IsPublic = true;
            return Write("public ");
        }
    }

    public T Internal {
        get {
            Root.CurrentCodeBlock.IsInternal = true;
            return Write("internal ");
        }
    }

    public T Static {
        get {
            Root.CurrentCodeBlock.IsStatic = true;
            return Write("static ");
        }
    }

    public T Abstract {
        get {
            Root.CurrentCodeBlock.IsAbstract = true;
            return Write("abstract ");
        }
    }

    public T Partial {
        get {
            Root.CurrentCodeBlock.IsPartial = true;
            return Write("partial ");
        }
    }

    /// <summary> Appends '\n', breaking current line. Activates indentation on the next line. </summary>
    /// <remarks> Name "br" stands for "break line" </remarks>
    public T br()
    {
        Root.Sb.Append('\n');
        EnableIndent();
        return this as T;
    }

    /// <summary>
    ///     Appends <see cref="value"/> on the current line, then appends '/n', breaking current line. Activates
    ///     indentation on the next line.
    /// </summary>
    public T Writeln(string value) => Write(value).br();

    /// <summary>
    ///     Indents (if needed), then appends <see cref="value"/> on the current line. Disables indentation on the next
    ///     line (if current line was indented).
    /// </summary>
    public T Write(string value)
    {
        if (Root.IsIndentNeeded) {
            Indent();
            DisableIndent();
        }

        Root.Sb.Append(value);
        return this as T;
    }

    /// <summary>
    ///     Appends indentation on the current line, according to the current indent level. If indent level = 0, will do
    ///     nothing.
    /// </summary>
    protected T Indent()
    {
        for (int j = 0; j < Root.IndentLevel; j++)
            Root.Sb.Append("	");
        return this as T;
    }
}
}
