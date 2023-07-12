using System;
using System.Text;
using JetBrains.Annotations;

namespace CodeGeneration {
[PublicAPI]
public class CodeBuilder : CodeBuilderBase<CodeBuilder> {

    private CodeBuilder(CodeBuilderGlue glue) : base(glue) { }

    public static CodeBuilder CreateDefaultBuilder() => new CodeBuilder(new CodeBuilderGlue(new StringBuilder()));

    // ---------------------------------------------------------
    // Code Builders
    // ---------------------------------------------------------

    /// <summary> By itself it does nothing, it only specifies what you are going to do next. </summary>
    public CodeBuilderModels.PropertyBuilder Property => new CodeBuilderModels.PropertyBuilder(Glue);


    // ---------------------------------------------------------
    // Code Blocks
    // ---------------------------------------------------------

    /// <summary> By itself it does nothing, it only specifies what you are going to do next. </summary>
    public CodeBlockModels.NamespaceBlock Namespace => new CodeBlockModels.NamespaceBlock(Glue); /// <inheritdoc cref="Namespace"/>
    public CodeBlockModels.InterfaceBlock Interface => new CodeBlockModels.InterfaceBlock(Glue); /// <inheritdoc cref="Namespace"/>
    public CodeBlockModels.ClassBlock     Class     => new CodeBlockModels.ClassBlock(Glue);     /// <inheritdoc cref="Namespace"/>
    public CodeBlockModels.MethodBlock    Method    => new CodeBlockModels.MethodBlock(Glue);    /// <inheritdoc cref="Namespace"/>
    public CodeBlockModels.EnumBlock      Enum      => new CodeBlockModels.EnumBlock(Glue);      /// <inheritdoc cref="Namespace"/>
    public CodeBlockModels.SwitchBlock    Switch => new CodeBlockModels.SwitchBlock(Glue);

    // ---------------------------------------------------------
    // Attributes & Indexers
    // ---------------------------------------------------------

    /// <summary> This is an attribute.  Writes $"[{self}] " </summary>
    public CodeBuilder PublicAPI            => Write("[PublicAPI] "); /// <inheritdoc cref="PublicAPI"/>
    public CodeBuilder SerializeField       => Write("[SerializeField] ");

    /// <summary> This is an attribute, with prefix "field: ".  Writes $"[field: {self}] " </summary>
    public CodeBuilder field_SerializeField => Write("[field: SerializeField] ");

    // I was playing with indexers, to make attributes more explicit, but it turned out not so useful.
    //public CodeBuilder this[[NotNull] params string[] attributes] => Write($"[{string.Join(", ", attributes)}]");
    //public CodeBuilder this[[NotNull] params CodeBuilder[] self] => self[^0];


    // ---------------------------------------------------------
    // Miscellaneous
    // ---------------------------------------------------------

    /// <summary> Writes file header. Also writes date and time of creation. </summary>
    public CodeBuilder Header(string className)
    {
        Writeln("//--------------------------------------------------------------------------------------");
        Writeln("// This file is generated. Modifications to this file won't be saved.");
        Writeln("// Creation time: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        Writeln("// If you want to make any permanent changes, go to the class " + className);
        Writeln("// At: ???");
        Writeln("//--------------------------------------------------------------------------------------");
        return this;
    }

    public CodeBuilder Summary(string summary) => Writeln("/// <summary>" + summary + "</summary>");

    /// <summary> Writes a simple commented separator for better code readability. </summary>
    public CodeBuilder TitledSeparator(string title)
    {
        Br();
        Writeln("//--------------------------------------------------------------------------------------");
        Writeln("// " + title);
        Writeln("//--------------------------------------------------------------------------------------");
        Br();
        return this;
    }

    /// <summary> Writes $"using {<see cref="namespace_"/>};" </summary>
    public CodeBuilder Using(string namespace_) => Writeln($"using {namespace_};");

}
}
