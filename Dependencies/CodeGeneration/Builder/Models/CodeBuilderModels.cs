using Graffiti.CodeGeneration.Internal.Helpers;
using JetBrains.Annotations;

namespace Graffiti.CodeGeneration {
public static class CodeBuilderModels {

    public class PropertyBuilder : CodeBuilderBase<PropertyBuilder> {

        public PropertyBuilder(CodeBuilderGlue glue) : base(glue) { }

        public PropertyBuilder Name([NotNull] string name)
            => Write(name);

        public PropertyBuilder Returns(string type = null)
            => Write(type.SelfOrDefault("void", true))
                   .Write(" ");

        public void Get([CanBeNull] string equateTo = null)
            => Write(" { get; }")
              .Write(equateTo == null ? "" : $" = {equateTo};")
              .Br();

        public void GetSet([CanBeNull] string equateTo = null)
            => Write(" { get; set; }")
              .Write(equateTo == null ? "" : $" = {equateTo};")
              .Br();

        public void GetPrivateSet([CanBeNull] string equateTo = null)
            => Write(" { get; private set; }")
              .Write(equateTo == null ? "" : $" = {equateTo};")
              .Br();

        public void GetReturnThis(string body)
            => Write(" { get { ")
              .Write(body)
              .Write("; return this; } }")
              .Br();

        public void Expression([NotNull] string body)
            => Write(" => ")
              .Write(body)
              .Write(";")
              .Br();

    }

}
}
