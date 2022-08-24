using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.CodeGeneration {
public static class CodeBlockModels {

    public class MethodBlock : CodeBlockBase<MethodBlock> {

        private (string type, string name)[] _params;

        public MethodBlock(CodeBuilderInfo root) : base(root) { }

        public MethodBlock Returns(string type = null)
            => ActionOrDefault(Writesp, type, "void", true);

        public MethodBlock Name([NotNull] string name)
            => ActionOrDefault(Write, name, "_UnnamedMethod_", false);

        public MethodBlock Params([NotNull] params (string type, string name)[] params_)
        {
            if (params_.Length == 0)
                return this;

            int lastIndex = params_.Length - 1;
            for (int i = 0; i < params_.Length; i++) {
                ActionOrDefault(Write, params_[i].type, "_UnmanagedType_", false);
                ActionOrDefault(Write, params_[i].name, "_UnmanagedParamName_", false);
                if (i < lastIndex)
                    Write(", ");
            }

            _params = params_;

            return this;
        }

        public (string type, string name) GetParam(int i)
        {
            if (i < 0 || i > _params.Length - 1) {
                // TODO: Handle Error
                Debug.LogError("!");
                return ("", "");
            }

            return _params[i];
        }

    }

    // public class EnumBlock : CodeBlockBase<EnumBlock> {
    //
    //     public EnumBlock(CodeBuilderInfo root, string name, string insideBlock = "") : base(
    //         root, CodeBlockInfo.Create(name), $"enum {name}", insideBlock) { }
    //
    //     public EnumBlock WriteDefaultMember()     => WriteMember("Default");
    //     public EnumBlock WriteMember(string name) => Writeln($"{name}, ");
    //
    // }
    //
    // public class SwitchBlock : CodeBlockBase<SwitchBlock> {
    //
    //     public SwitchBlock(CodeBuilderInfo root) : base(root) { }
    //
    //     public SwitchBlock DefaultCase()     => Write("default:");
    //     public SwitchBlock Case(string name) => Write($"case {name}: ");
    //     public SwitchBlock Return(string value)   => Writeln($"return {value};");
    //     public SwitchBlock Break()                => Writeln("break;");
    //
    // }

}
}
