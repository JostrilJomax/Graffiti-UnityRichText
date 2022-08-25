using System;
using System.Text;

namespace Graffiti.CodeGeneration {
public class CodeBuilder : CodeBuilderBase<CodeBuilder> {

    private CodeBuilder(CodeBuilderGlue glue) : base(glue) { }


    // ---------------------------------------------------------
    // Attributes & Indexers
    // ---------------------------------------------------------

    public CodeBuilder PublicAPI            => Write("[PublicAPI] ");
    public CodeBuilder SerializeField       => Write("[SerializeField] ");
    public CodeBuilder field_SerializeField => Write("[field: SerializeField] ");

    public static CodeBuilder CreateDefaultBuilder() => new CodeBuilder(new CodeBuilderGlue(new StringBuilder()));


    // ---------------------------------------------------------
    // Miscellaneous
    // ---------------------------------------------------------

    public CodeBuilder Header(string className)
    {
        Writeln("//--------------------------------------------------------------------------------------");
        Writeln("// This file is generated. Modifications to this file won't be saved.");
        Writeln("// Last generated: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        Writeln("// If you want to make any permanent changes to this file, go to the class " + className);
        Writeln("// At: ???");
        Writeln("//--------------------------------------------------------------------------------------");
        return this;
    }

    public CodeBuilder Using(string @namespace) => Writeln($"using {@namespace};");

    // These indexers could potentially make the attributes more explicit in code generation, but currently they don't.
    //public CodeBuilder this[[NotNull] params string[] attributes] => Write($"[{string.Join(", ", attributes)}]");
    //public CodeBuilder this[[NotNull] params CodeBuilder[] self] => self[^0];


    // ---------------------------------------------------------
    // Code Builders
    // ---------------------------------------------------------

    public CodeBuilderModels.PropertyBuilder Property => new CodeBuilderModels.PropertyBuilder(Glue);


    // ---------------------------------------------------------
    // Code Blocks (Code Block is a construction of format: " 'Header' { 'Content' } 'AfterBracketClose' ")
    // ---------------------------------------------------------

    public CodeBlockModels.NamespaceBlock Namespace => new CodeBlockModels.NamespaceBlock(Glue);
    public CodeBlockModels.InterfaceBlock Interface => new CodeBlockModels.InterfaceBlock(Glue);
    public CodeBlockModels.ClassBlock     Class     => new CodeBlockModels.ClassBlock(Glue);
    public CodeBlockModels.MethodBlock    Method    => new CodeBlockModels.MethodBlock(Glue);
    public CodeBlockModels.EnumBlock      Enum      => new CodeBlockModels.EnumBlock(Glue);
    public CodeBlockModels.SwitchBlock    Switch    => new CodeBlockModels.SwitchBlock(Glue);

}
}
