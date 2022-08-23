namespace Graffiti.CodeGeneration {
public class CodeBlockInfo : MemberModifiers {
    public string Name { get; private set; }

    public static CodeBlockInfo Create(string name) => new CodeBlockInfo { Name = name };
}
}
