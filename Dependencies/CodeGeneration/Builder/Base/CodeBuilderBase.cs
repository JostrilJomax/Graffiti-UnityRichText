using System;
using Graffiti.CodeGeneration.Internal.Helpers;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.CodeGeneration {
/// <summary> This class contains all non-generic shared methods. See <see cref="CodeBuilderBase{T}"/> for more info. </summary>
[PublicAPI]
public abstract class CodeBuilderBase {

    /// <summary> All shared info/objects are stored here. </summary>
    /// <remarks> This property is called <see cref="Glue"/>, as it connects all <see cref="CodeBuilder"/>s.
    /// It also contains shared StringBuilder. </remarks>
    public CodeBuilderGlue Glue { get; }

    /// <summary> You can configurate <see cref="CodeBuilder"/> behaviour from here </summary>
    public CodeBuilderConfiguration Config => Glue.BuilderConfig;

    /// <summary> Returns info about current block. If you are not in any block, will return <see cref="CodeBlockInfo.CreateRootBlockInfo"/> </summary>
    /// <remarks> You can also pass it as a string to any method. It will convert itself to the name of the current block
    /// see <see cref="CodeBlockInfo.ToString"/></remarks>
    public CodeBlockInfo Self => Glue.CurrentCodeBlock;

    public override string ToString() => Glue.Sb.ToString();

    protected CodeBuilderBase(CodeBuilderGlue glue) => Glue = glue;

    protected void IncreaseIndent() => Glue.IndentLevel++;
    protected void DecreaseIndent() => Glue.IndentLevel = Math.Max(--Glue.IndentLevel, 0);
    protected void Append(string value) => Glue.Sb.Append(value);
    protected void Append(char value)   => Glue.Sb.Append(value);
    protected void Indent(bool forceIndent = false)
    {
        if (!forceIndent && !Glue.WillIndentNewLine)
            return;
        for (int i = 0; i < Glue.IndentLevel; i++)
            Append("    ");
        Glue.WillIndentNewLine = false;
    }
}

/// <summary>
///     This class contains all generic shared methods.
///     <para/>
///     This class exists to allow contained methods to directly return provided type T, which allows anyone who implements
///     this class to be able to use method chaining (Fluent API) without the need of constant cast to their type.
/// </summary>
[PublicAPI]
public abstract class CodeBuilderBase<T> : CodeBuilderBase where T : CodeBuilderBase<T> {

    protected CodeBuilderBase(CodeBuilderGlue glue) : base(glue) { }

    /// <summary> Writes <b>lowercase self with space</b> afterwards: $"{self} " </summary>
    public T Public    => Write(Glue.CurrentCodeBlock.WillUsePublic   ).Write(" "); /// <inheritdoc cref="Public"/>
    public T Internal  => Write(Glue.CurrentCodeBlock.WillUseInternal ).Write(" "); /// <inheritdoc cref="Public"/>
    public T Protected => Write(Glue.CurrentCodeBlock.WillUseProtected).Write(" "); /// <inheritdoc cref="Public"/>
    public T Private   => Write(Glue.CurrentCodeBlock.WillUsePrivate  ).Write(" "); /// <inheritdoc cref="Public"/>
    public T Static    => Write(Glue.CurrentCodeBlock.WillUseStatic   ).Write(" "); /// <inheritdoc cref="Public"/>
    public T Partial   => Write(Glue.CurrentCodeBlock.WillUsePartial  ).Write(" "); /// <inheritdoc cref="Public"/>
    public T Abstract  => Write(Glue.CurrentCodeBlock.WillUseAbstract ).Write(" ");

    /// <summary> Writes '\n', breaking current line. Activates indentation on the next line. </summary>
    /// <remarks> Name "Br" stands for "Break line", terming "br" is also used in html tag &lt;br/&gt; </remarks>
    public T Br()
    {
        Append('\n');
        Glue.IsExplicitStartOfNewLine = true;
        Glue.WillIndentNewLine = true;
        return this as T;
    }

    /// <summary> Writes <see cref="value"/>, then calls <see cref="Br"/>. Activates indentation on the next line. </summary>
    public T Writeln(string value) => Write(value).Br();

    /// <summary>
    ///     Writes indentation on newline, then writes <see cref="value"/>.
    ///     On each line indentation is applied only once.
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
}
}
