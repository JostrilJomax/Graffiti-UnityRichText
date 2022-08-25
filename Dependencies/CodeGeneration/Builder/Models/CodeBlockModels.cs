using Graffiti.CodeGeneration.Internal.Helpers;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.CodeGeneration {
public static class CodeBlockModels {

    public class NamespaceBlock : CodeBlockBase<NamespaceBlock> {

        public NamespaceBlock(CodeBuilderGlue glue) : base(glue) { }

        public NamespaceBlock Name(string name)
            => Write("namespace ")
              .WriteBlockName(name);

    }

    public class ClassBlock : CodeBlockBase<ClassBlock> {

        public ClassBlock(CodeBuilderGlue glue) : base(glue) { }

        public ClassBlock Name(string name)
            => Write("class ")
              .WriteBlockName(name);

        public ClassBlock Nameof<T>() => Name(typeof(T).Name);

        public ClassBlock Inherit(string inheritMe)
            => Write(" : ")
              .Write(inheritMe);

        public ClassBlock Inherit(string inheritMe, string inheritMe2)
            => Inherit(inheritMe.CommaSpace(inheritMe2));
        public ClassBlock Inherit(string inheritMe, string inheritMe2, string inheritMe3)
            => Inherit(inheritMe.CommaSpace(inheritMe2).CommaSpace(inheritMe3));

    }

    public class InterfaceBlock : CodeBlockBase<InterfaceBlock> {

        public InterfaceBlock(CodeBuilderGlue glue) : base(glue) { }

        public InterfaceBlock Name(string name)
            => Write("interface ")
              .WriteBlockName(name);

    }

    public class MethodBlock : CodeBlockBase<MethodBlock> {

        private (string type, string name)[] _params;

        public MethodBlock(CodeBuilderGlue glue) : base(glue) { }

        public MethodBlock Name([NotNull] string name) => WriteBlockName(name);

        public MethodBlock Returns(string type = null)
            => Write(type.SelfOrDefault("void", true))
              .Write(" ");

        public MethodBlock Params(params (string type, string name)[] params_)
        {
            if (params_ == null || params_.Length == 0) {
                Write("()");
                return this;
            }

            Write("(");
            int lastIndex = params_.Length - 1;
            for (int i = 0; i < params_.Length; i++) {
                Append(params_[i].type.SelfOrDefault("_UnnamedType_", false));
                Append(' ');
                Append(params_[i].name.SelfOrDefault("_UnnamedParamName_", false));
                if (i < lastIndex)
                    Append(", ");
            }

            Append(")");
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

    public class EnumBlock : CodeBlockBase<EnumBlock> {

        public EnumBlock(CodeBuilderGlue glue) : base(glue) { }

        public EnumBlock Name(string name)
            => Write("enum ")
              .WriteBlockName(name);

        public EnumBlock DefaultMember()     => Member("Default");
        public EnumBlock Member(string name) => Write($"{name},").Br();
    }

    public class SwitchBlock : CodeBlockBase<SwitchBlock> {

        public SwitchBlock(CodeBuilderGlue glue) : base(glue) { }

        public SwitchBlock Value(string value)
            => Write("switch (")
              .WriteBlockName(value)
              .Write(")");

        public SwitchBlock DefaultCase()        => Write("default: ");
        public SwitchBlock Case(string name)    => Write($"case {name}: ");
        public SwitchBlock Return()             => Write("return;").Br();
        public SwitchBlock Return(string value) => Write($"return {value};").Br();
        public SwitchBlock Break()              => Write("break;").Br();

    }

}
}
