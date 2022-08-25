using JetBrains.Annotations;

namespace Graffiti.CodeGeneration {
/// <summary>
///     Information about code block. Contains <see cref="MemberInfo"/>, <see cref="Name"/> and so on.
///     Code block has the following format:
///     <para/>
///     ...header... { ...body... } ...afterword...
/// </summary>
[PublicAPI]
public class CodeBlockInfo : MemberInfo {

    /// <summary> The name of the current block. For classes it will be the class name, for methods - method name and so on. </summary>
    public string Name { get; internal set; }

    /// <summary> Will return the name of this code block </summary>
    public override string ToString() => Name;
    public static implicit operator string(CodeBlockInfo self) => self.ToString();

    /// <summary> The info about the root block. Root block has no body and brackets, it exists to prevent errors. </summary>
    internal static CodeBlockInfo CreateRootBlockInfo()
        => new CodeBlockInfo {
            Name = "Root",
        };
}
}
