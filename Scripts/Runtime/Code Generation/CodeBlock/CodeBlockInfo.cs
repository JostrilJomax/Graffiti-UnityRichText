namespace Graffiti.CodeGeneration {
public class CodeBlockInfo : MemberInfo {

    public string Name { get; internal set; }

    public static CodeBlockInfo CreateRootBlockInfo()
        => new CodeBlockInfo {
            Name = "Root",
        };
}
}
