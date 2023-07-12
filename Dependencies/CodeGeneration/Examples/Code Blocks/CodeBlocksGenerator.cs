using CodeGeneration;

namespace CodeGeneration.Examples {
public static class CodeBlocksGenerator {

    public static string Generate()
    {
        var b = CodeBuilder.CreateDefaultBuilder();

        // Header that tells that this file is generated
        b.Header(nameof(CodeBlocksGenerator)).Br();

        // Namespace is not required, but it makes life easier
        b.Namespace.Name("Generated").Body(() => {

            // Empty static class
            b.Public.Static.Class.Name("MyClass").Body(() => { });

            b.Br();

            // enum with 2 members
            b.Internal.Enum.Name("MyEnum").Body(Enum => {
                Enum.DefaultMember();
                Enum.Member("NotDefault");
            });

            b.Br();

            // Empty interface
            b.Public.Interface.Name("IInterface").Body(() => { });

            b.Br();

            // Class that inherits interface
            b.Public.Class.Name("SpecialClass").Inherit("IInterface").Body(() => { });

        });

        return b.ToString();

    }

}
}
